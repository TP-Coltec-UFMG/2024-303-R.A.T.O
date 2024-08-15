using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private GameObject InteragirTutorial;
    [SerializeField] private string InteragirTutorialText;

    void Awake(){
        this.InteragirTutorial.GetComponent<TMP_Text>().text = InteragirTutorialText;
    }

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
        while(!Input.GetKeyDown(KeyCode.M)){
            yield return null;
        }
        
        GameController.Instance.Save(this.transform.position);
    }
}
