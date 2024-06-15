using UnityEngine;
using UnityEngine.UI;
 
public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private Dialogue Dialogue;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            SpeakTo();
        }
    }
 
    // Trigger dialogue for this actor
    public void SpeakTo()
    {
        DialogueManager.Instance.StartDialogue(Dialogue.RootNodes);
    }
}

