using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }

    [SerializeField] private GameObject DialogueParent; 
    [SerializeField] private TMP_Text DialogTitleText, DialogBodyText;
    [SerializeField] private GameObject responseButtonPrefab;
    [SerializeField] private Transform responseButtonContainer;
    [SerializeField] private Image Icon;
    private List<DialogueNode> currentDialogueNodes;
    private int currentIndex;
    [SerializeField] private float delay;

    private void Awake(){
        if (Instance == null){
            Instance = this;
        }else{
            Destroy(gameObject);
        }

        HideDialogue();
    }

    public void StartDialogue(string title, List<DialogueNode> nodes, Sprite iconSprite){
        ShowDialogue();
        Icon.sprite = iconSprite;
        DialogTitleText.text = title;
        currentDialogueNodes = nodes;
        currentIndex = 0;
        UpdateDialogueUI();
    }

    private void UpdateDialogueUI()
    {
        StartCoroutine(TypeTextUI(currentDialogueNodes[currentIndex].dialogueText));

        foreach (Transform child in responseButtonContainer){
            Destroy(child.gameObject);
        }

        int i = 0;
        foreach (DialogueResponse response in currentDialogueNodes[currentIndex].responses){
            if(i == 0){
                GameObject buttonObj = Instantiate(responseButtonPrefab, new Vector3(190, 30, 0), Quaternion.identity, responseButtonContainer);
                buttonObj.GetComponentInChildren<TextMeshProUGUI>().text = response.responseText;

                buttonObj.GetComponent<Button>().onClick.AddListener(() => SelectResponse(response));
            }else if(i == 1){
                GameObject buttonObj = Instantiate(responseButtonPrefab, new Vector3(500, 30, 0), Quaternion.identity, responseButtonContainer);
                buttonObj.GetComponentInChildren<TextMeshProUGUI>().text = response.responseText;

                buttonObj.GetComponent<Button>().onClick.AddListener(() => SelectResponse(response));
            }
            
            i++;
        }
    }

    public void SelectResponse(DialogueResponse response){
        if (response.nextDialogueNodes.Count > 0){
            currentDialogueNodes = response.nextDialogueNodes;
            currentIndex = 0;
            UpdateDialogueUI();
        }else{
            HideDialogue();
        }
    }

    void Update(){
        if(IsDialogueActive() && Input.GetKeyDown(KeyCode.Return)){
            if (currentIndex + 1 < currentDialogueNodes.Count){
                currentIndex++;
                UpdateDialogueUI();
            }else{
                HideDialogue();
            }
        }
    }

    public void HideDialogue(){
        DialogueParent.SetActive(false);
    }

    private void ShowDialogue(){
        DialogueParent.SetActive(true);
    }

    public bool IsDialogueActive(){
        return DialogueParent.activeSelf;
    }

    IEnumerator TypeTextUI(string text){
        DialogBodyText.text = "";
        foreach (char letter in text){
            DialogBodyText.text += letter;
            yield return new WaitForSeconds(delay);            
        }
    }
}
