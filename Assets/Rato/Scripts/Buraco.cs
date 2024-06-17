using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buraco : MonoBehaviour
{
    [SerializeField] private string NewScene;

    void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.tag == "Player"){
            GameController.Instance.SetNewSceneOnKeyPress(NewScene);
        }
    }
}
