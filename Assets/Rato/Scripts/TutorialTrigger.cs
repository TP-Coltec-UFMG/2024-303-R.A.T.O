using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialTrigger : MonoBehaviour
{
    [SerializeField] [TextArea(1, 10)] private string TutorialText;
    [SerializeField] GameObject TutorialPanel;
    [SerializeField] TMP_Text TutorialTextUI;

    void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.tag == "Player"){
            StartCoroutine(Tutorial());
        }
    }

    IEnumerator Tutorial(){
        //Time.timeScale = 0;
        TutorialPanel.SetActive(true);
        TutorialTextUI.text = "";
        GetComponent<BoxCollider2D>().enabled = false;
        
        foreach (char c in TutorialText){
            TutorialTextUI.text += c;
            yield return new WaitForSecondsRealtime(0.1f);
        }
        
        TutorialPanel.SetActive(false);
        //Time.timeScale = 1;

        if(gameObject.tag == "TutorialFinal"){
            foreach(GameObject freeze in GameObject.FindGameObjectsWithTag("Freeze")){
                freeze.SetActive(false);
            }

            GameObject.Find("Buraco").GetComponent<BoxCollider2D>().enabled = true;
        }
        
        gameObject.SetActive(false);
    }
}
