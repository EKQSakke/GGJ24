using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCVisuals : MonoBehaviour
{
    public SpriteRenderer BodyRenderer;
    public SpriteRenderer HeadRenderer;
    public SpriteRenderer FaceRenderer;

    private Sprite assignedNeutralFace;
    private Sprite assignedHappyFace;
    private Sprite assignedAngryFace;

    public void SetupVisuals()
    {
        assignedNeutralFace = GameManager.GameSettings.NeutralFaces.GetRandomElementFromList();
        assignedHappyFace = GameManager.GameSettings.HappyFaces.GetRandomElementFromList();
        assignedAngryFace = GameManager.GameSettings.AngryFaces.GetRandomElementFromList();

        HeadRenderer.sprite = GameManager.GameSettings.Heads.GetRandomElementFromList();
        SetMood(NPCMood.Neutral);
    }

    public void SetMood(NPCMood mood)
    {
        switch (mood) 
        {
            case NPCMood.Happy:
                FaceRenderer.sprite = assignedHappyFace;
                break;
            case NPCMood.Angry:
                FaceRenderer.sprite = assignedAngryFace; 
                break;
            default:
                FaceRenderer.sprite = assignedNeutralFace;
                break;
        }
    }

}

public enum NPCMood
{
    Neutral,
    Happy,
    Angry
}