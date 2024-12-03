using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public bool isAiming;
    public bool hasWeapon;
    [SerializeField] private GameObject rifleModel;
    [SerializeField] private GameObject aimingCam;
    [SerializeField] private Transform mainCamera;
    [SerializeField] private float shootingRange;
    [SerializeField] private float fireRateCd;
    [SerializeField] private bool isAttackCd;
    [SerializeField] private GameObject muzzleFlashVfx;
    [SerializeField] private GameObject bulletImpactVfx;

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

        if(!isAiming) return;

        if(Input.GetMouseButtonDown(0)){
            playerManager.playerAnimation.animator.SetBool(playerManager.playerAnimation.IS_AIMING_ANIM_PARAM, true);
            playerManager.playerAnimation.animator.CrossFade(playerManager.playerAnimation.FIRING_RIFLE_ANIM, 0.2f);
        }
        if(Input.GetMouseButtonUp(0)){
            muzzleFlashVfx.SetActive(false);
            playerManager.playerAnimation.animator.SetBool(playerManager.playerAnimation.IS_AIMING_ANIM_PARAM, false);
            playerManager.playerAnimation.animator.CrossFade(playerManager.playerAnimation.FIRING_RIFLE_ANIM, 0.2f);
        }

        if(Input.GetMouseButton(0)){
            Shoot();
        }
    }

    public void OnGettingWeapon(){
        hasWeapon = true;
        rifleModel.SetActive(true);
        playerManager.playerAnimation.animator.SetInteger(playerManager.playerAnimation.WEAPON_STATE_ANIM_PARAM, 1);
    }
    
    void AimShot(){
        if(!hasWeapon) return;

        if(Input.GetMouseButtonDown(1)){
            playerManager.playerAnimation.animator.CrossFade(playerManager.playerAnimation.RIFLE_AIM_IDLE_ANIM, 0.2f);
            playerManager.playerAnimation.animator.SetBool(playerManager.playerAnimation.IS_AIMING_ANIM_PARAM, true);
            isAiming = true;
            aimingCam.SetActive(true);
        } else if(Input.GetMouseButtonUp(1)){
            muzzleFlashVfx.SetActive(false);
            playerManager.playerAnimation.animator.SetBool(playerManager.playerAnimation.IS_AIMING_ANIM_PARAM, false);
            isAiming = false;
            aimingCam.SetActive(false);
        }
    }

    IEnumerator ShootCoroutine(){
        isAttackCd = true;
        yield return new WaitForSeconds(fireRateCd);
        isAttackCd = false;
    }
      void Shoot(){

        if (isAttackCd) return;

        muzzleFlashVfx.SetActive(true);
        StartCoroutine(ShootCoroutine());
        if(Physics.Raycast(mainCamera.position, mainCamera.forward, out RaycastHit hit , shootingRange)){
            var effect =  Instantiate(bulletImpactVfx, hit.point, Quaternion.identity);
            effect.transform.LookAt(transform);
            print(hit.collider.gameObject.name);
        }
    }
}
