using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "GameData/NPCDialogue", order = 1)]
public class NPCDialogueData : GameData
{
    public List<string> Dialogue = new List<string>();
}
