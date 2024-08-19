using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOffTrigger : MonoBehaviour
{
    [SerializeField] private List<GameObject> Activate;
    [SerializeField] private bool OnOff;

    void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.tag == "Player"){
            foreach(GameObject activate in Activate){
                activate.SetActive(OnOff);
            }
        }
    }
}
