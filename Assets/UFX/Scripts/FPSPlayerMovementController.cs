using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSPlayerMovementController : MonoBehaviour
{
    [Header("Character")]
    [SerializeField] private CharacterController controller;
    [SerializeField] private float speed = 12f;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheckGO;
    [SerializeField] private LayerMask groundMask;

    float xMove;
    float zMove;
    Vector3 movement;

    float gravity = -9.81f;
    Vector3 velocity;

    bool isGrounded;
    float groundDistance = 0.4f;

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheckGO.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0) {
            velocity.y = -2f;
        }

        xMove = Input.GetAxis("Horizontal");
        zMove = Input.GetAxis("Vertical");

    }

    private void FixedUpdate() {
 
        movement = transform.right * xMove + transform.forward * zMove;
        controller.Move(movement * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);       
    }
}
