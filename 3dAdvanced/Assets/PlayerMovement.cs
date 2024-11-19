using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    private CharacterController _characterController;

    private void Awake() {
        _characterController = GetComponent<CharacterController>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Movement();    
    }

    void Movement(){
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 move = new Vector3(x,0,z).normalized;

        _characterController.Move(move * movementSpeed * Time.deltaTime);
    }
}
