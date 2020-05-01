using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// usage: applicera detta skript på en kamera
public class SmoothFollowCamera : MonoBehaviour
{

    [SerializeField] private Transform target;      // objektet som denna kamera ska följa mjukt
    [SerializeField] private float cameraSpeed = 15f;
    // public float zOffset = 22f;
    [SerializeField] private FloatVariable zOffsetScriptableObject;

    [SerializeField] private float zOffsetInit;     // Startvärdet för zOffset

    [SerializeField] private bool isFollow = true;


    void Start() {
        zOffsetScriptableObject.value = zOffsetInit;
    }


    // Update is called once per frame
    void Update()
    {
        // undvik köra detta om vi inte har någon referens till något objekt
        if (target) {
            Vector3 newPos = transform.position;
            newPos.x = target.position.x;
            newPos.z = target.position.z - zOffsetScriptableObject.value;

            if (!isFollow) 
                transform.position = newPos; 
            else 
                transform.position = Vector3.Lerp(transform.position, newPos, cameraSpeed * Time.deltaTime);
        }
    }
}
