using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buraco : MonoBehaviour
{
    [SerializeField] private string NewScene;
    private bool sair;

    void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.tag == "Player"){
            StartCoroutine(WaitForKeyPress(NewScene));
        }
    }

    void OnTriggerExit2D(){
        StopAllCoroutines();
    }

    private IEnumerator WaitForKeyPress(string sceneName){
        while (!Input.GetKeyDown(KeyCode.E)){
            yield return null;
        }
        
        GameController.Instance.ChangeScene(sceneName);
    }
}
