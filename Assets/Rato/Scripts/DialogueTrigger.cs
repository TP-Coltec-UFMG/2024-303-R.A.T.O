using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
 
public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private GameObject SpeakToTutorial;
    [SerializeField] private List<Dialogue> Dialogues;
    [SerializeField] private bool Optional;

    public void SpeakTo()
    {
        if(GameController.Instance.GetRatoHumanity() == 0){
            DialogueManager.Instance.StartDialogue(Dialogues[0].RootNodes, this.gameObject);
        }else{
            DialogueManager.Instance.StartDialogue(Dialogues[1].RootNodes, this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.tag == "Player"){
            if(this.Optional){
                SpeakToTutorial.SetActive(true);
                StartCoroutine(WaitForKeyPress());
            }else{
                SpeakTo();
            }
        }
    }

    void OnTriggerExit2D(){
        if(SpeakToTutorial != null){
            SpeakToTutorial.SetActive(false);
        }
        StopAllCoroutines();
    }

    private IEnumerator WaitForKeyPress(){
        while(!UserInput.Instance.InteractInput){
            yield return null;
        }
        
        SpeakTo();
    }
}

