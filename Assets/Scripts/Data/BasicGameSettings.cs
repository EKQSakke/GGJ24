using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Game Settings", menuName = "Custom Scriptables/Game Settings")]
public class BasicGameSettings : ScriptableObject
{

    public NPCMoodSprites NeutralSprites;
    public NPCMoodSprites HappySprites;
    public NPCMoodSprites AngrySprites;
    public List<Sprite> Heads;
    public float NPCScaleVariance = 0.15f;

}

[Serializable]
public class NPCMoodSprites
{
    public List<Sprite> EyeSprites;
    public List<Sprite> MouthSprites;
    public List<Sprite> NoseSprites;

    public NPCVisualLook GiveRandomSprites()
    {
        NPCVisualLook randomLook = new NPCVisualLook()
        {
            Eyes = EyeSprites.GetRandomElementFromList(),
            Mouth = MouthSprites.GetRandomElementFromList(),
            Nose = NoseSprites.GetRandomElementFromList()
        };

        return randomLook;
    }

}