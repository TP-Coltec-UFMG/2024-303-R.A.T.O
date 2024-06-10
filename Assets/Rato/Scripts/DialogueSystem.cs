using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueSystem : MonoBehaviour
{
    private static List<string> lines;
    [SerializeField] private GameObject DialogueBox;
    [SerializeField] private TMP_Text DialogueText;
    [SerializeField] private float TextSpeed, Delay, FinalDelay;

    void Start(){
        lines = new List<string>();
        Debug.Log("aaa"); 
    }
    
    public void Initialize(){
        this.DialogueBox.SetActive(true);
    }

    public void NewLine(string text){
        if(lines != null){
            lines.Add(text);
            StartCoroutine(WriteText());
        }else{
            //Debug.Log("aaa");
        }
    }

    IEnumerator WriteText(){
        foreach(string text in lines){
            foreach(char c in text){
                this.DialogueText.text += c;
                if(c == ',' || c == '.'){
                    yield return new WaitForSeconds(this.Delay);
                }else{
                    yield return new WaitForSeconds(this.TextSpeed);   
                }
            }

            yield return new WaitForSeconds(this.FinalDelay);  
        }
        
        this.DialogueBox.SetActive(false);
    }

    public void Clean(){
        this.DialogueText.text = "";
    }
}
