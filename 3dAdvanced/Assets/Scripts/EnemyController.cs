using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;


public enum ENEMY_STATE{
    LOOKING_FOR_WEAPON,
    LOOKING_FOR_AMMO,
    LOOKING_FOR_ENEMY,
    ATTACKING_ENEMY,
    IS_DEAD
}
public class EnemyController : Ammo
{
    [SerializeField] private ENEMY_STATE eNEMY_STATE;
    [SerializeField] private float movementSpeed;
    private NavMeshAgent navMeshAgent;


    private Health enemyTargetHealth;
    public Transform enemyTarget;
    public Transform weaponLootParent;
    public Transform charckterParent;
    public Transform ammoParent;
    [SerializeField] private GameObject rifleModel;
    [SerializeField] private GameObject muzzleFlashVfx;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private GameObject bulletPrefab;

    [SerializeField] private float startShootingRange;
    [SerializeField] private float stopShootingRange;

    [SerializeField] private float shootingCd;
    [SerializeField] private float defaultShootingCd;
    
    private EnemyManager enemyManager;

    private void Awake() {
       enemyManager = GetComponent<EnemyManager>();
       navMeshAgent = GetComponent<NavMeshAgent>();
       navMeshAgent.speed = movementSpeed;       
    }

    private void Start() {
        weaponLootParent = GameManager.Instance.weaponLootParent;
        charckterParent = GameManager.Instance.charckterParent;
        ammoParent = GameManager.Instance.ammoLootParent;
        eNEMY_STATE = ENEMY_STATE.LOOKING_FOR_WEAPON;
        CurrentAmmo = defaultAmmo;
    }

    private void Update() {
        if(enemyManager.enemyHealth.isDead) return;

        switch(eNEMY_STATE){
            case ENEMY_STATE.LOOKING_FOR_WEAPON: 
            try
            {
            enemyTarget = FindNearestTarget(weaponLootParent).transform; 
            navMeshAgent.destination = enemyTarget.position;
            }
            catch (System.Exception e)
            {
                print("Target not found");
            }
             break;
            case ENEMY_STATE.LOOKING_FOR_ENEMY: 
              try
              {
              enemyTarget = FindNearestTarget(charckterParent).transform; 
              navMeshAgent.destination = enemyTarget.position;  
              if(Vector3.Distance(transform.position, enemyTarget.position) < startShootingRange){
                 if (CurrentAmmo > 0) {
                SetAttackingEnemyState();
            } else {
                eNEMY_STATE = ENEMY_STATE.LOOKING_FOR_AMMO;
                enemyTarget = null;
            }
              } 
            }
              catch (System.Exception)
              {
                print("Target not found");
              } 
            ; break;
            case ENEMY_STATE.LOOKING_FOR_AMMO:
            try
            {
                enemyTarget = FindNearestTarget(ammoParent).transform;
                navMeshAgent.destination = enemyTarget.position;
                if(isAmmo){
                    eNEMY_STATE = ENEMY_STATE.LOOKING_FOR_ENEMY;
                    enemyTarget = null;
                }

            }
            catch (System.Exception)
            {
                print("Target not Found Ammo");
            } break;
            case ENEMY_STATE.ATTACKING_ENEMY:  
               Shoot();
                if (CurrentAmmo <= 0) {
                    eNEMY_STATE = ENEMY_STATE.LOOKING_FOR_AMMO;
                    navMeshAgent.isStopped = false;
                    enemyTarget = null;
                    break;
                 }
               if(Vector3.Distance(transform.position, enemyTarget.position) > stopShootingRange){
                  SetLookingForEnemyState();
              } 
            ; break;
            case ENEMY_STATE.IS_DEAD:  
             
            ; break;
        }
    }

    public void SetLookingForEnemyState(){
        if (enemyManager.enemyHealth.isDead) return;

        if(enemyTarget != null) {
            if (enemyManager.enemyHealth.isDead) return;

            if(enemyTarget.TryGetComponent(out Health health)){
                enemyTargetHealth = health;
                enemyTargetHealth.onDead.AddListener(TargetIsDead);
            }
        }
        eNEMY_STATE = ENEMY_STATE.LOOKING_FOR_ENEMY;
        rifleModel.SetActive(true);
        enemyManager.enemyAnimation.animator.CrossFade(enemyManager.enemyAnimation.RIFLE_RUN_ANIM, 0.2f);
        muzzleFlashVfx.SetActive(false);
        navMeshAgent.isStopped = false;
    }

    public void SetAttackingEnemyState(){
        if (enemyManager.enemyHealth.isDead) return;

        enemyTargetHealth = enemyTarget.GetComponent<Health>();
        enemyTargetHealth.onDead.AddListener(TargetIsDead);
        
        eNEMY_STATE = ENEMY_STATE.ATTACKING_ENEMY;
        rifleModel.SetActive(true);
        enemyManager.enemyAnimation.animator.CrossFade(enemyManager.enemyAnimation.FIRING_RIFLE_ANIM, 0.2f);
        muzzleFlashVfx.SetActive(true);
        navMeshAgent.isStopped = true;
    }
    public void SetEnemyIsDead(){

        if (enemyTargetHealth != null) {
            enemyTargetHealth.onDead.RemoveListener(TargetIsDead);
        }
        eNEMY_STATE = ENEMY_STATE.IS_DEAD;
        rifleModel.SetActive(true);
        muzzleFlashVfx.SetActive(false);
        navMeshAgent.isStopped = true;
    }

    void TargetIsDead(){
        enemyTargetHealth.onDead.RemoveListener(TargetIsDead);
        SetLookingForEnemyState();
    }

    GameObject FindNearestTarget(Transform targetParent){
        var distanceNearestTarget = Mathf.Infinity;
        GameObject nearestTarget = null;

        foreach (Transform target in targetParent){
            if (target == transform) continue;

            if(target.TryGetComponent(out Health health)){
                if(health.isDead) continue;
            }
            
            var distanceCurrentTarget = (target.transform.position - transform.position).sqrMagnitude;

            if(distanceCurrentTarget < distanceNearestTarget){
                distanceNearestTarget = distanceCurrentTarget;
                nearestTarget = target.gameObject;
            }
        }
        return nearestTarget;
    }

    void Shoot(){
        transform.LookAt(enemyTarget);
        shootingCd -= Time.deltaTime;
        if (CurrentAmmo <= 0){
            muzzleFlashVfx.SetActive(false);
            enemyManager.enemyAnimation.animator.CrossFade(enemyManager.enemyAnimation.RIFLE_RUN_ANIM, 0.2f);
            eNEMY_STATE = ENEMY_STATE.LOOKING_FOR_AMMO;
            navMeshAgent.isStopped = false;
            return;
        }
        if (shootingCd <= 0){
            CurrentAmmo--;
            shootingCd = defaultShootingCd;
            Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
        }
    }
}
