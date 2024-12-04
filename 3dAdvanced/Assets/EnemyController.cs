using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    private NavMeshAgent navMeshAgent;
    private EnemyManager enemyManager;
    public Transform enemyTarget;

    private void Awake() {
       enemyManager = GetComponent<EnemyManager>();
       navMeshAgent = GetComponent<NavMeshAgent>();
       navMeshAgent.speed = movementSpeed;       
    }

    private void Update() {
        navMeshAgent.destination = enemyTarget.position;
    }
  
}
