using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueSystem : MonoBehaviour
{
    [SerializeField] private GameObject DialogueBox;
    [SerializeField] private TMP_Text DialogueText;
    [SerializeField] private float TextSpeed;

    public void Initialize(){
        this.DialogueBox.SetActive(true);
    }

    public void Finish(){
        this.DialogueBox.SetActive(false);
    }

    public void WriteText(string text){
        StartCoroutine(WriteTextPause(text));
    }

    IEnumerator WriteTextPause(string text){
        foreach (char c in text){
            this.DialogueText.text += c;
            yield return new WaitForSeconds(this.TextSpeed);   
        }
    }

    public void Clean(){
        this.DialogueText.text = "";
    }
}
