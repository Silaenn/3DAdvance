using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
    private EnemyManager enemyManager;

    private void Awake() {
        enemyManager = GetComponent<EnemyManager>();
    }
    public override void Dead()
    {
        base.Dead();
        enemyManager.enemyAnimation.animator.CrossFade(enemyManager.enemyAnimation.DEAD_ANIM, 0.2f);
    }
}
