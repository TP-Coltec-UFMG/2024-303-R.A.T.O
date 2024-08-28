using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
 
public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private GameObject Peixe;
    [SerializeField] private List<Dialogue> Dialogues;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            //SpeakTo();
        }
    }

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
            if(gameObject.tag == "Peixe" && this.Peixe != null){
                Peixe.SetActive(true);
            }

            SpeakTo();
        }
    }
}

