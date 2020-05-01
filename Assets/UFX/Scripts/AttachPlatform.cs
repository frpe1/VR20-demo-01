using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachPlatform : MonoBehaviour
{
    [SerializeField] private GameObject targetObject;

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            targetObject.transform.parent = transform;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "Player") {
            targetObject.transform.parent = null;
        }
    }
}
