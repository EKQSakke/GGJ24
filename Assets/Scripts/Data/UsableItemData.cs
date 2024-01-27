using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Usable Item", menuName = "Custom Scriptables/Usable Item Data")]
public class UsableItemData : GameData
{
    public GameObject Prefab;
    public UsableItemType ItemType;
    public InteractionMode InteractionMode;
}

public enum InteractionMode
{
    None,
    Click,
    Drag
}

public enum UsableItemType
{
    None,
    Food,
    Drug,
    Coffee,
    YesStamp,
    NoStamp
}