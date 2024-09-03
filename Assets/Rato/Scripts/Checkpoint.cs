using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Checkpoint : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.tag == "Player"){
            GameController.Instance.Save(this.transform.position);
        }
    }
    
    void OnTriggerExit2D(){
        gameObject.SetActive(false);
    }
}
