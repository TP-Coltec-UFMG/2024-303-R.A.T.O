using UnityEngine;
using UnityEngine.UI;
 
public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private GameObject Peixe;
    [SerializeField] private Dialogue Dialogue;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            //SpeakTo();
        }
    }

    public void SpeakTo()
    {
        DialogueManager.Instance.StartDialogue(Dialogue.RootNodes, this.gameObject);
    }

    void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.tag == "Player"){
            if(gameObject.tag == "Peixe" && this.Peixe != null){
                Peixe.SetActive(true);
            }

            SpeakTo();
        }
    }
}

