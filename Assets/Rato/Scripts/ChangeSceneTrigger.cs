using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSceneTrigger : MonoBehaviour
{
    [HideInInspector] public string NewScene;
    [HideInInspector] public Vector3 RatoPosition;

    void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.tag == "Player"){
            GameController.Instance.ChangeScene(this.NewScene, this.RatoPosition);
        }
    }
}
