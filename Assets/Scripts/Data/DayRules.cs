using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Rules", menuName = "GameData/Rules", order = 1)]

public class DayRules : GameData
// Start is called before the first frame update
{
    public string City;
    public string Occupation;
    public UsableItemType itemNeeded = UsableItemType.None;

}
