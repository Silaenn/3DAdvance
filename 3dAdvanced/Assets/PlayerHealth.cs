using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : Health
{
    private PlayerManager playerManager;
    [SerializeField] private Image healthBarImage;

    private void Awake() {
        playerManager = GetComponent<PlayerManager>();
    }

    public override void OnChangeHealth()
    {
        base.OnChangeHealth();
        healthBarImage.fillAmount = CurrentHp / maxHp;
    }
    public override void Dead()
    {
        base.Dead();
        playerManager.playerShoot.OnDead();
        playerManager.playerAnimation.animator.CrossFade(playerManager.playerAnimation.DEATH_ANIM, 0.2f);
    }
}
