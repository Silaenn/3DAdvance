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

         if(move.magnitude > 0.1f){
            float targetAngle = Mathf.Atan2(move.x, move.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref cureentVelocity, rotationSmoothTime);

            transform.rotation = Quaternion.Euler(0, angle, 0);

            Vector3 moveDIrection = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;

            _characterController.Move(moveDIrection.normalized * movementSpeed * Time.deltaTime);
         }

         playerVelocity.y += gravity * Time.deltaTime;
         _characterController.Move(playerVelocity * Time.deltaTime);
    }
}
