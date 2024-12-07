using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{
    private PlayerManager playerManager;

    private void Awake() {
        playerManager = GetComponent<PlayerManager>();
    }
    public override void Dead()
    {
        base.Dead();
        playerManager.playerShoot.OnDead();
        playerManager.playerAnimation.animator.CrossFade(playerManager.playerAnimation.DEATH_ANIM, 0.2f);
    }
}
