using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCameraLimits : MonoBehaviour
{
    [SerializeField] private float min, max;

    void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.tag == "Player"){
            CameraController.instance.SetLimits(min, max);
        }
    }
}
