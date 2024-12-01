using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public bool isAiming;
    public bool hasWeapon;
    [SerializeField] private GameObject rifleModel;
    [SerializeField] private GameObject aimingCam;
    private PlayerManager playerManager;
    void Start()
    {
        playerManager = GetComponent<PlayerManager>();
    }

    void Update()
    {   AimShot();
        ShootInput();
    }

    void ShootInput(){
        if(Input.GetMouseButton(0)){
            playerManager.playerAnimation.animator.CrossFade(playerManager.playerAnimation.FIRING_RIFLE_ANIM, 0.2f);
        }
    }

    void AimShot(){
        if(!hasWeapon) return;

        if(Input.GetMouseButtonDown(1)){
            playerManager.playerAnimation.animator.SetBool(playerManager.playerAnimation.IS_AIMING_ANIM_PARAM, true);
            isAiming = true;
            aimingCam.SetActive(true);
        } else if(Input.GetMouseButtonUp(1)){
            playerManager.playerAnimation.animator.SetBool(playerManager.playerAnimation.IS_AIMING_ANIM_PARAM, false);
            isAiming = false;
            aimingCam.SetActive(false);
        }
    }
}
