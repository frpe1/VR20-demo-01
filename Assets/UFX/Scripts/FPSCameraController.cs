using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCameraController : MonoBehaviour
{
    [SerializeField] private Transform playerBody;

    [SerializeField] private  float sensitivity = 5.0f;

    [SerializeField] private float smoothing = 2.0f;
    
    private float _smoothing = 0f;          // för optimering

    private float _sensitivity_smooth = 0f; // för optimering

    // get the incremental value of mouse moving
    private Vector2 mouseLook;
    
    // smooth the mouse moving
    private Vector2 smoothV;    

    // Start is called before the first frame update
    void Start()
    {
        // Lås muspositionen och dölj den. 
        Cursor.lockState = CursorLockMode.Locked;

        // För optimering för att undvika göra divisionberäkningar varje gång, då detta bara är ett konstant värde
        _smoothing = 1 / smoothing; 
    }

    // Update is called once per frame
    void Update()
    {
        // När vi trycker escape knappen så kan vi få tillbaka musen igen
        if (Input.GetKeyDown("escape")) {
            
            // turn on the cursor
            Cursor.lockState = CursorLockMode.None;
        }
        else {
            _sensitivity_smooth = sensitivity * smoothing * Time.deltaTime;

            // Läs av musens rörelse. 
            var md = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
            md = Vector2.Scale(md, new Vector2(_sensitivity_smooth, _sensitivity_smooth));

            // interpolera mellan dessa värden
            smoothV.x = Mathf.Lerp(smoothV.x, md.x, _smoothing);
            smoothV.y = Mathf.Lerp(smoothV.y, md.y, _smoothing);
            
            // öka stegvis camera-look view rörelsen. 
            mouseLook += smoothV;

            // förhindrar att man kan röra kameran mer än 90 grader uppåt/nedåt vertikalt
            mouseLook.y = Mathf.Clamp(mouseLook.y, -90f, 90f);
            
            // utför rotationen vertikalt
            transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);

            // utför rotationen horisontellt
            playerBody.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, playerBody.transform.up);

        }
    }
}
