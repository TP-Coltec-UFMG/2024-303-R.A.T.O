using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 
[System.Serializable]
public class DialogueNode
{
    public string title;
    public Sprite iconSprite;
    [TextArea(2, 10)] public string dialogueText;
    public List<DialogueResponse> responses;
    internal bool IsLastNode()
    {
        return responses.Count <= 0;
    }
}   


