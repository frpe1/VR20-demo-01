using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCameraController : MonoBehaviour
{

    [SerializeField] private Transform target;
    [SerializeField] private Vector3 position;

    private Vector3 offset; // för in/ut zoomning
    [SerializeField] bool useOffset; // Sätter om vi ska använda offset eller inte

    // Start is called before the first frame update
    void Start()
    {   
        // Alternativ fix position eller gör så att vi får kameran rör sig fix offset position i förhållande till target
        if (useOffset)
            offset = target.position - transform.position;
        else
            transform.position = position;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        // Alternativ 1, fix position eller rörlig position som beror på annat objekt
        //transform.position = position;

        // Alternativ 2, gör att man låter kameran följa med på fix position men med en viss offset/förskjutning
        if (useOffset)
            transform.position = target.position - offset;

        // Alternativ 1 och 2, Lookat gör så man följerföljer target objektet hela tiden med view.
        transform.LookAt(target);
        */
    }

    private void FixedUpdate() {
     
    }

    private void LateUpdate() {
        // Alternativ 1, fix position eller rörlig position som beror på annat objekt
        //transform.position = position;

        // Alternativ 2, gör att man låter kameran följa med på fix position men med en viss offset/förskjutning
        if (useOffset)
            transform.position = target.position - offset;

        // Alternativ 1 och 2, Lookat gör så man följerföljer target objektet hela tiden med view.
        transform.LookAt(target);           
    }
}
