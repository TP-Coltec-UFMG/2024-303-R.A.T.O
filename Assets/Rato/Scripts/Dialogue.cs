using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue/Dialogue Asset")]
public class Dialogue : ScriptableObject
{
    //First node of the conversation
    public List<DialogueNode> RootNodes;
}
