using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Cutscene : MonoBehaviour
{
    [SerializeField] private GameObject Panel;
    [SerializeField] private TMP_Text TextUI;
    [SerializeField] [TextArea(1, 10)] private string Text;
    
    void Digitar(){
        StartCoroutine(DigiTalento());
    }
    
    IEnumerator DigiTalento(){
        Panel.SetActive(true);
        TextUI.text = "";

        foreach (char c in Text){
            TextUI.text += c;
            yield return new WaitForSecondsRealtime(0.1f);
        }

        while(!Input.GetKeyDown(KeyCode.Return)){
            yield return null;
        }

        GameController.Instance.ChangeScene("casateste");
    }
}
