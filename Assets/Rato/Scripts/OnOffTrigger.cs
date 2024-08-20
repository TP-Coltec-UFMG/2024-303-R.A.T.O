using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOffTrigger : MonoBehaviour
{
    [SerializeField] private List<GameObject> Activate;
    [SerializeField] private bool OnOff;

    void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.tag == "Player" && gameObject.tag != "Adeus"){
            foreach(GameObject activate in Activate){
                activate.SetActive(OnOff);
            }
        }else if(gameObject.tag == "Adeus" && collider.gameObject.tag == "Suborno"){
            foreach(GameObject activate in Activate){
                activate.SetActive(OnOff);
            }
        }
    }
}
