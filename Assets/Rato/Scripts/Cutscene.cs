using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Cutscene : MonoBehaviour
{
    [SerializeField] private GameObject Panel;
    [SerializeField] private TMP_Text TextUI;
    [SerializeField] [TextArea(1, 10)] private string Text1, Text2;
    
    void Digitar(){
        StartCoroutine(DigiTalento());
    }
    
    IEnumerator DigiTalento(){
        Panel.SetActive(true);
        TextUI.text = "";

        foreach (char c in Text1){
            TextUI.text += c;
            yield return new WaitForSecondsRealtime(0.1f);
        }

        while(!Input.GetKeyDown(KeyCode.Return)){
            yield return null;
        }

        TextUI.text = "";

        foreach (char c in Text2){
            TextUI.text += c;
            yield return new WaitForSecondsRealtime(0.1f);
        }

        while(!Input.GetKeyDown(KeyCode.Return)){
            yield return null;
        }

        GameController.Instance.ChangeScene("casateste");
    }
}
