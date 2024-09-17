using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportTrigger : MonoBehaviour
{
    [SerializeField] private GameObject InteragirTutorial;
    [SerializeField] private Transform TeleportThis;
    [SerializeField] private Vector3 NewPosition;

    void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.tag == "Player"){
            InteragirTutorial.SetActive(true);
            StartCoroutine(WaitForKeyPress());
        }
    }

    void OnTriggerExit2D(){
        InteragirTutorial.SetActive(false);
        StopAllCoroutines();
    }

    private IEnumerator WaitForKeyPress(){
        while(!UserInput.Instance.InteractInput){
            yield return null;
        }
        
        Teleport();
    }

    private void Teleport(){
        TeleportThis.position = NewPosition;
    }
}
