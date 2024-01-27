using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "GameData/NPCData", order = 1)]
public class NPCData : GameData
{
    public GameObject NPCPrefab;
    public string Name;
    public string City;
    public string Occupation;
    public enum Need 
    {Money, Food, Drugs };

}
