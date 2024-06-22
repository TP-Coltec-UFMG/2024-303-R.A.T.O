using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Typer : MonoBehaviour
{
    public bool isTyping {get; private set;}
    public static Typer Instance;

    private Coroutine typingCoroutine;
    private string[] currentTexts;
    private int currentIndex;
    private TMP_Text currentTextUI;
    private GameObject currentPanel;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update(){
        if(isTyping && Input.GetKeyDown(KeyCode.Return)){
            if (typingCoroutine != null){
                StopCoroutine(typingCoroutine);
                currentTextUI.text = currentTexts[currentIndex];
                typingCoroutine = null;
            }else if (currentIndex + 1 < currentTexts.Length){
                currentIndex++;
                typingCoroutine = StartCoroutine(TypeText(currentPanel, currentTextUI, currentTexts[currentIndex]));
            }else{
                EndTyping();
            }
        }
    }

    public void TypeNSkip(GameObject Panel, TMP_Text TextUI, string[] texts){
        if(isTyping){
            return;
        }

        currentPanel = Panel;
        currentTextUI = TextUI;
        currentTexts = texts;
        currentIndex = 0;

        Panel.SetActive(true);
        typingCoroutine = StartCoroutine(TypeText(Panel, TextUI, texts[0]));
    }

    IEnumerator TypeText(GameObject Panel, TMP_Text TextUI, string text){
        isTyping = true;
        TextUI.text = "";

        foreach (char c in text)
        {
            TextUI.text += c;
            yield return new WaitForSecondsRealtime(0.1f);
        }

        typingCoroutine = null;
    }

    void EndTyping(){
        isTyping = false;
        currentPanel.SetActive(false);
        currentPanel = null;
        currentTextUI = null;
        currentTexts = null;
    }
}
