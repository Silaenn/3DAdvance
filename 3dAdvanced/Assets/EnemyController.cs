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
    public Transform playerTarget;
    public Transform enemyTarget;
    public Transform weaponLootParent;
    [SerializeField] private GameObject rifleModel;

    private EnemyManager enemyManager;

    private void Awake() {
       enemyManager = GetComponent<EnemyManager>();
       navMeshAgent = GetComponent<NavMeshAgent>();
       navMeshAgent.speed = movementSpeed;       
    }

    private void Start() {
        weaponLootParent = GameManager.Instance.weaponLootParent;
        eNEMY_STATE = ENEMY_STATE.LOOKING_FOR_WEAPON;
    }

    private void Update() {

        switch(eNEMY_STATE){
            case ENEMY_STATE.LOOKING_FOR_WEAPON: enemyTarget = FindNearestTarget().transform; 
            navMeshAgent.destination = enemyTarget.position; break;
            case ENEMY_STATE.LOOKING_FOR_ENEMY:  
              navMeshAgent.destination = playerTarget.position;
            ; break;
            case ENEMY_STATE.ATTACKING_ENEMY:  ; break;
            case ENEMY_STATE.IS_DEAD:  ; break;
        }
    }

    public void SetLookingForEnemyState(){
        eNEMY_STATE = ENEMY_STATE.LOOKING_FOR_ENEMY;
        rifleModel.SetActive(true);
        enemyManager.enemyAnimation.animator.CrossFade(enemyManager.enemyAnimation.RIFLE_RUN_ANIM, 0.2f);
    }

    GameObject FindNearestTarget(){
        var distanceNearestTarget = Mathf.Infinity;
        GameObject nearestTarget = null;

        foreach (Transform weapon in weaponLootParent){
            var distanceCurrentTarget = (weapon.transform.position - transform.position).sqrMagnitude;

            if(distanceCurrentTarget < distanceNearestTarget){
                distanceNearestTarget = distanceCurrentTarget;
                nearestTarget = weapon.gameObject;
            }

        }

        return nearestTarget;
    }
  
}
