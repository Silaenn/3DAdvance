using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float movementSpeed;
    private CharacterController _characterController;

    [Header("Gravity")]
    [SerializeField] private float gravity;
    [SerializeField] private Transform groundChecker;
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private LayerMask groundLayer;
     private Vector3 playerVelocity;

    [Header("Rotation")]
    [SerializeField] private float rotationSmoothTime;
    [SerializeField] private Transform cameraTransform; 
     private float cureentVelocity;
     private Vector2 mouseCamRotation;
     [SerializeField] private float mouseCamRotationSpeed;

     private PlayerManager playerManager;

    private void Awake() {
        _characterController = GetComponent<CharacterController>();
        playerManager = GetComponent<PlayerManager>();
    }

    void Update()
    {
        Movement();    
    }

    bool IsGrounded(){
        return Physics.CheckSphere(groundChecker.position, groundCheckRadius, groundLayer);
    }

    void Movement(){
        if(IsGrounded()) playerVelocity.y = -2f;

        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 move = new Vector3(x,0,z).normalized;

        float moveAnim = new Vector2(x, z).magnitude;
        playerManager.playerAnimation.animator.SetFloat(playerManager.playerAnimation.MOVE_ANIM_PARAM, moveAnim, 0.1f, Time.deltaTime);

        bool hasPlayerInputMoving = move.magnitude >= 0.1f;

        if(playerManager.playerShoot.isAiming){
         if(hasPlayerInputMoving){
            MoveCharacter(AimMovement(move));
         }
            MouseCamRotation();

        } else {
         if(hasPlayerInputMoving){
            MoveCharacter(FreeMovement(move));
         }
        }
         playerVelocity.y += gravity * Time.deltaTime;
         _characterController.Move(playerVelocity * Time.deltaTime);
    }

     Vector3 AimMovement(Vector3 move){
        move = transform.TransformDirection(move);
        return move;
    }

    Vector3 FreeMovement(Vector3 move){
            float targetAngle = Mathf.Atan2(move.x, move.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref cureentVelocity, rotationSmoothTime);

            transform.rotation = Quaternion.Euler(0, angle, 0);

            Vector3 moveDIrection = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
            return moveDIrection;
    }

    void MouseCamRotation(){
        mouseCamRotation.x += Input.GetAxis("Mouse X") * mouseCamRotationSpeed;
        mouseCamRotation.y += Input.GetAxis("Mouse Y") * mouseCamRotationSpeed;

        mouseCamRotation.y = Mathf.Clamp(mouseCamRotation.y, -10, 10);

        transform.rotation = Quaternion.Euler(-mouseCamRotation.y, mouseCamRotation.x, 0);
    }

    void MoveCharacter(Vector3 move){
         _characterController.Move(move.normalized * movementSpeed * Time.deltaTime);
    }

  
}
