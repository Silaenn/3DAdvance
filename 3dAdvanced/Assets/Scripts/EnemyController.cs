using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;


public enum ENEMY_STATE{
    LOOKING_FOR_WEAPON,
    LOOKING_FOR_ENEMY,
    ATTACKING_ENEMY,
    IS_DEAD
}
public class EnemyController : MonoBehaviour
{
    [SerializeField] private ENEMY_STATE eNEMY_STATE;
    [SerializeField] private float movementSpeed;
    private NavMeshAgent navMeshAgent;


    public Transform enemyTarget;
    public Transform weaponLootParent;
    public Transform charckterParent;
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
        eNEMY_STATE = ENEMY_STATE.LOOKING_FOR_WEAPON;
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
                  SetAttackingEnemyState();
              } 
            }
              catch (System.Exception)
              {
                print("Target not found");
              } 
            ; break;
            case ENEMY_STATE.ATTACKING_ENEMY:  
               Shoot();
               if(Vector3.Distance(transform.position, enemyTarget.position) > stopShootingRange){
                  SetLookingForEnemyState();
              } 
            ; break;
            case ENEMY_STATE.IS_DEAD:  ; break;
        }
    }

    public void SetLookingForEnemyState(){
        eNEMY_STATE = ENEMY_STATE.LOOKING_FOR_ENEMY;
        rifleModel.SetActive(true);
        enemyManager.enemyAnimation.animator.CrossFade(enemyManager.enemyAnimation.RIFLE_RUN_ANIM, 0.2f);
        muzzleFlashVfx.SetActive(false);
        navMeshAgent.isStopped = false;
    }

    public void SetAttackingEnemyState(){
        eNEMY_STATE = ENEMY_STATE.ATTACKING_ENEMY;
        rifleModel.SetActive(true);
        enemyManager.enemyAnimation.animator.CrossFade(enemyManager.enemyAnimation.FIRING_RIFLE_ANIM, 0.2f);
        muzzleFlashVfx.SetActive(true);
        navMeshAgent.isStopped = true;
    }

    GameObject FindNearestTarget(Transform targetParent){
        var distanceNearestTarget = Mathf.Infinity;
        GameObject nearestTarget = null;

        foreach (Transform target in targetParent){
            if (target == transform) continue;
            
            var distanceCurrentTarget = (target.transform.position - transform.position).sqrMagnitude;

            if(distanceCurrentTarget < distanceNearestTarget){
                distanceNearestTarget = distanceCurrentTarget;
                nearestTarget = target.gameObject;
            }
        }
        return nearestTarget;
    }

    void Shoot(){
        shootingCd -= Time.deltaTime;
        if (shootingCd <= 0){
            shootingCd = defaultShootingCd;
            Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
        }
    }
  
}
