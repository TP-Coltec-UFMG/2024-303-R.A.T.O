using UnityEngine;
using UnityEngine.UI;
 
public class Actor : MonoBehaviour
{
    [SerializeField] private string Name;
    [SerializeField] private Dialogue Dialogue;
    [SerializeField] private Sprite Icon;

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
        DialogueManager.Instance.StartDialogue(Name, Dialogue.RootNodes, Icon);
    }
}

