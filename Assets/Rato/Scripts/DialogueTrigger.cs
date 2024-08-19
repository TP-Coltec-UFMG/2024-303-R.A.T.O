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
        DialogueManager.Instance.StartDialogue(Dialogues[GameController.Instance.GetRatoHumanity()].RootNodes, this.gameObject);
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

