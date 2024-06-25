using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Interactable : MonoBehaviour
{
    [SerializeField] [TextArea(2, 10)] private string[] Description;
    [SerializeField] private TMP_Text DescriptionUI;
    [SerializeField] private GameObject Panel, InteragirTutorial;
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
        while (!Input.GetKeyDown(KeyCode.E)){
            yield return null;
        }
        
        Typer.Instance.TypeNSkip(this.Panel, DescriptionUI, Description);
    }
}
