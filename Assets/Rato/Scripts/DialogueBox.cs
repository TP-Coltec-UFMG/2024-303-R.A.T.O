using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DialogueBox : MonoBehaviour
{
    [SerializeField] private TMP_Text DialogueText, CharacterName;
    [SerializeField] private Image CharacterIcon;
    [SerializeField] private float Delay;

    public DialogueBox Initialize(string name, Image icon){
        this.CharacterName.name = name;
        this.CharacterIcon = icon;

        return this;
    }

    public void PlayText(string text){
        StartCoroutine(TypeText(text));
    }

    IEnumerator TypeText(string text){
        foreach(char c in text){
            this.DialogueText.text += c;

            if(c == '.' || c == ','){
                yield return new WaitForSeconds(this.Delay * 3);
            }else{
                yield return new WaitForSeconds(this.Delay);
            }
        }

        yield return new WaitForSeconds(this.Delay * 5);
    }
}
