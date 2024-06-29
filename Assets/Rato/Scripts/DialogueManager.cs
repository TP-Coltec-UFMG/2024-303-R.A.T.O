using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
    private Coroutine typingCoroutine;
    private GameObject dialogueTrigger;

    private void Awake(){
        if (Instance == null){
            Instance = this;
        }else{
            Destroy(gameObject);
        }

        HideDialogue();
    }

    public void StartDialogue(List<DialogueNode> nodes, GameObject trigger){
        GameController.Instance.StopGame();

        ShowDialogue();

        currentDialogueNodes = nodes;
        currentIndex = 0;
        UpdateDialogueUI();
        
        if(trigger != null){
            trigger.SetActive(false);
        }
    }

    private void UpdateDialogueUI()
    {
        if(currentDialogueNodes[currentIndex].iconSprite != null && Icon != null){
            Icon.sprite = currentDialogueNodes[currentIndex].iconSprite;
        }
        if(currentDialogueNodes[currentIndex].title != null && DialogTitleText != null){
            DialogTitleText.text = currentDialogueNodes[currentIndex].title;
        }

        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }
        typingCoroutine = StartCoroutine(TypeTextUI(currentDialogueNodes[currentIndex].dialogueText));

        if(responseButtonContainer != null){
            foreach (Transform child in responseButtonContainer){
                Destroy(child.gameObject);
            }
        }

        if(responseButtonContainer != null){
            int i = 0;
            foreach (DialogueResponse response in currentDialogueNodes[currentIndex].responses){
                GameObject buttonObj;
                if (i == 0){
                    buttonObj = Instantiate(responseButtonPrefab, new Vector3(60, 30, 0), Quaternion.identity, responseButtonContainer);
                }else if (i == 1){
                    buttonObj = Instantiate(responseButtonPrefab, new Vector3(400, 30, 0), Quaternion.identity, responseButtonContainer);
                } else {
                    continue;
                }

                buttonObj.GetComponentInChildren<TextMeshProUGUI>().text = response.responseText;
                buttonObj.GetComponent<Button>().onClick.AddListener(() => SelectResponse(response));

                i++;
            }
        }
    }

    public void SelectResponse(DialogueResponse response){
        if (response.nextDialogueNodes.Count > 0){
            currentDialogueNodes = response.nextDialogueNodes;
            currentIndex = 0;
            UpdateDialogueUI();
        }else{
            HideDialogue();
            GameController.Instance.Resume();
        }
    }

    void Update(){
        if(IsDialogueActive() && Input.GetKeyDown(KeyCode.Return)){
            if (typingCoroutine != null)
            {
                StopCoroutine(typingCoroutine);
                DialogBodyText.text = currentDialogueNodes[currentIndex].dialogueText;
                typingCoroutine = null;
            }
            else if (currentIndex + 1 < currentDialogueNodes.Count){
                currentIndex++;
                UpdateDialogueUI();
            }else{
                HideDialogue();
                GameController.Instance.Resume();
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
            yield return new WaitForSecondsRealtime(delay);
        }
        typingCoroutine = null;
    }
}

