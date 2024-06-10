using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    [SerializeField] private DialogueBox DialogueBoxPrefab;

    public void NewDialogueBox(string text, string name, Image icon){
        DialogueBox db = Instantiate(this.DialogueBoxPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        db.Initialize(name, icon);

        db.PlayText(text);
    }
}
