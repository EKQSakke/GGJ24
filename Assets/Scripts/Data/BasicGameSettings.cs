using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Game Settings", menuName = "Custom Scriptables/Game Settings")]
public class BasicGameSettings : ScriptableObject
{

    public List<Sprite> AngryFaces;
    public List<Sprite> NeutralFaces;
    public List<Sprite> HappyFaces;
    public List<Sprite> Heads;

}