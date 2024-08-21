using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PermanentInteractable : MonoBehaviour
{
    [SerializeField] [TextArea(2, 10)] private string Description;
    [SerializeField] private TMP_Text DescriptionUI;
    [SerializeField] private GameObject Panel, InteragirTutorial, CreditosText;
    [SerializeField] private string InteragirTutorialText;

    void Awake(){
        this.InteragirTutorial.GetComponent<TMP_Text>().text = InteragirTutorialText;
    }

    void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.tag == "Player"){
            InteragirTutorial.SetActive(true);
            StartCoroutine(WaitForKeyPress());
        }
    }

    void OnTriggerExit2D(){
        InteragirTutorial.SetActive(false);
        StopAllCoroutines();
    }

    private IEnumerator WaitForKeyPress(){
        while(!UserInput.Instance.InteractInput){
            yield return null;
        }
        
        StartCoroutine(TypeText());
    }

    IEnumerator TypeText(){
        Panel.SetActive(true);
        DescriptionUI.text = "";

        foreach (char c in Description)
        {
            DescriptionUI.text += c;
            yield return new WaitForSecondsRealtime(0.2f);
        }

        while(!Input.GetKeyDown(KeyCode.Return)){
            yield return null;
        }

        Panel.SetActive(false);

        CreditosText.GetComponent<Creditos>().restart = true;
        MenuController.Instance.OpenMenuInGame();
        MenuController.Instance.Creditos();
    }
}
