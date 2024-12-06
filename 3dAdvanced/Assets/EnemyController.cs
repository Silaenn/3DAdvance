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
    public Transform charckterParent;
    [SerializeField] private GameObject rifleModel;

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
              navMeshAgent.destination = playerTarget.position;  
              }
              catch (System.Exception)
              {
                print("Target not found");
              } 
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
  
}
