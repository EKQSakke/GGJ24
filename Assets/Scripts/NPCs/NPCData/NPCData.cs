using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "GameData/NPCData", order = 1)]
public class NPCData : GameData
{
    public GameObject NPCPrefab;
    public GameObject InteractionParticlePrefab;
    public GameObject BadInteractionParticlePrefab;
    public string Name;
    public string City;
    public string Occupation;
    public AnimationCurve StressGenerationCurve;
    public float StressOnSuccess;
    public float StressOnFail;
    public UsableItemType ItemNeeded = UsableItemType.None;
    public Sprite RelatedHat;
}