using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Game Settings", menuName = "Custom Scriptables/Game Settings")]
public class BasicGameSettings : ScriptableObject
{
    [Header("NPC variation")]
    public List<string> Names = new List<string>();
    public List<Sprite> ClosedEyes;
    public NPCMoodSprites NeutralSprites;
    public NPCMoodSprites HappySprites;
    public NPCMoodSprites AngrySprites;
    public List<Sprite> Heads;
    public List<Sprite> Bodies;
    public List<Sprite> Hats;
    public float NPCScaleVariance = 0.15f;

    [Header("Sounds")]
    public List<AudioClip> CustomerVoiceClips;

    private void OnValidate()
    {
        var stream = new StreamReader("Assets/Lista_nimilista_nimistä.txt");
        while (!stream.EndOfStream)
            Names.Add(stream.ReadLine());
    }

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