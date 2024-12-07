using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
    private EnemyManager enemyManager;
    [SerializeField] private Collider aliveColider;
    [SerializeField] private Collider deadColider;

    private void Awake() {
        enemyManager = GetComponent<EnemyManager>();
    }
    public override void Dead()
    {
        base.Dead();
        aliveColider.enabled = false;
        deadColider.enabled = true;
        enemyManager.enemyAnimation.animator.CrossFade(enemyManager.enemyAnimation.DEAD_ANIM, 0.2f);
    }
}
