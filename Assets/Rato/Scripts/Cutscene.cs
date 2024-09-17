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
    [SerializeField] private GameObject PularButton;
    private AudioSource audioSource;

    //[SerializeField] private Audio alma, cadeira, caindo, passos, procurando, rato, ratoeira, subindo;

    private void Start(){
        this.audioSource = GetComponent<AudioSource>();
    }
    public void CutsceneNarration(){
        PularButton.SetActive(false);
        Typer.Instance.TypeNSkip(Panel, TextUI, Texts);
        StartCoroutine(CheckTypingCompletion());
    }

    private IEnumerator CheckTypingCompletion(){
        while (Typer.Instance.isTyping){
            yield return null;
        }
        
        GameController.Instance.ChangeScene(NewScene, new Vector3(0, 0, 0));
    }

    public void PlayAudio(AudioClip audio){
        audioSource.clip = audio;
        audioSource.Play();
    }
}

