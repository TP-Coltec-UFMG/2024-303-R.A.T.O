using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Cutscene : MonoBehaviour
{
    [SerializeField] private GameObject Panel;
    [SerializeField] private TMP_Text TextUI;
    [SerializeField] [TextArea(1, 10)] private string[] Texts;
    [SerializeField] private string NewScene;

    public void CutsceneNarration(){
        Typer.Instance.TypeNSkip(Panel, TextUI, Texts);
        StartCoroutine(CheckTypingCompletion());
    }

    private IEnumerator CheckTypingCompletion(){
        while (Typer.Instance.isTyping){
            yield return null;
        }
        
        GameController.Instance.ChangeScene(NewScene);
    }
}

