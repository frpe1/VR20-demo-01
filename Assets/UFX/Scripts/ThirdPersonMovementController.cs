using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovementController : MonoBehaviour
{

    public float moveSpeed;        // Anger hur snabbt man rör på sig.
    public float jumpForce;        // Anger hur stor kraften är när man hoppar uppåt
    public float gravityMagnitude; // Skapar större gravitation 

    // Alternativ 2
    private CharacterController chController; // Använder character controller , med det följer också kollision detektion automatiskt
    private Vector3 moveDirection; // flyttningsriktningen


    private float x, y, z, xrot, yrot, zrot; // Interna koordinatvariabler som behövs för separera update och fixedupdate för beräkningarna inkl avläsning av kontroller


    // Start is called before the first frame update
    void Start()
    {
        chController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");



        // Alternativ 1 ) Flyter på bra i Update() med fix kamera position
        // moveDirection = new Vector3( x * moveSpeed, 0 , z * moveSpeed);
        // Alternativ 1b ) Flyter på bra i Update() med fix kamera position
        // moveDirection = new Vector3( x * moveSpeed * Time.deltaTime, 0 , z * moveSpeed * Time.deltaTime);
        // transform.Translate(moveDirection);


        // Alternativ 2 ) Använder character controller komponent, vi får då med collision detection, Flyter bra i Update()
        moveDirection = new Vector3(x * moveSpeed, moveDirection.y, z * moveSpeed);

        // Alternativ 2 ) Beräkna fysiken själv för JUMP och gravity, följer formeln y = y0 - g*t
        // Kollar även om vi befinner oss på marken, för det är endast då vi kan hoppa
        // detta flyter väldigt bra i förhållande till kameran om kameran befinner sig i latestUpdate()
        /*
        if (Input.GetButtonDown("Jump") && chController.isGrounded) {
            moveDirection.y = jumpForce;
        }
        
        // Alternativ 2 fungerar, men är lite jerky moves
        moveDirection.y = moveDirection.y + (Physics.gravity.y * gravityMagnitude * Time.deltaTime);

        chController.Move(moveDirection * Time.deltaTime);
        */

        // Alternativ 3, vi stoppar istället för som update2 emellan raderna för moveDirection
        // och moveDirection.y så stoppar vi in här avläsning av jump
        if (Input.GetButtonDown("Jump") && chController.isGrounded) {
            moveDirection.y = jumpForce;
        }
    }

    private void FixedUpdate() {
        // Alternativ 3 )  samma som alternativ 2, men placeras under fixedUpdate istället, flyter också på väldigt bra
        moveDirection = new Vector3(x * moveSpeed, moveDirection.y, z * moveSpeed);


        
        moveDirection.y = moveDirection.y + (Physics.gravity.y * gravityMagnitude * Time.deltaTime);

        chController.Move(moveDirection * Time.deltaTime);      
    }

    private void LateUpdate() {
        
    }
}
