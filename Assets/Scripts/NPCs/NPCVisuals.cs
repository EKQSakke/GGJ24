using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCVisuals : MonoBehaviour
{
    public SpriteRenderer BodyRenderer;
    public SpriteRenderer HeadRenderer;

    public SpriteRenderer EyesRenderer;
    public SpriteRenderer MouthRenderer;
    public SpriteRenderer NouseRenderer;
    public SpriteRenderer HatRenderer;

    private RandomNPCLooks myLook;
    private Vector3? defaultScale;

    public void SetupVisuals(Sprite workHat = null)
    {
        if (defaultScale == null)
        {
            defaultScale = transform.localScale;
        }

        CreateRandomLook();
        HeadRenderer.sprite = myLook.BaseHead;
        BodyRenderer.sprite = myLook.Body;
        HatRenderer.sprite = workHat;
        SetMood(NPCMood.Neutral);
    }

    public void SetMood(NPCMood mood)
    {
        switch (mood) 
        {
            case NPCMood.Happy:
                EyesRenderer.sprite = myLook.HappyLook.Eyes;                
                NouseRenderer.sprite = myLook.HappyLook.Nose;
                MouthRenderer.sprite = myLook.HappyLook.Mouth;
                break;
            case NPCMood.Angry:
                EyesRenderer.sprite = myLook.AngryLook.Eyes;                
                NouseRenderer.sprite = myLook.AngryLook.Nose;
                MouthRenderer.sprite = myLook.AngryLook.Mouth;
                break;
            default:
                EyesRenderer.sprite = myLook.NeutralLook.Eyes;                
                NouseRenderer.sprite = myLook.NeutralLook.Nose;
                MouthRenderer.sprite = myLook.NeutralLook.Mouth;
                break;
        }
    }

    private void CreateRandomLook()
    {
        myLook = new RandomNPCLooks();
        myLook.BaseHead = GameManager.GameSettings.Heads.GetRandomElementFromList();
        myLook.Body = GameManager.GameSettings.Bodies.GetRandomElementFromList();

        myLook.NeutralLook = GameManager.GameSettings.NeutralSprites.GiveRandomSprites();
        myLook.AngryLook = GameManager.GameSettings.AngrySprites.GiveRandomSprites();
        myLook.HappyLook = GameManager.GameSettings.HappySprites.GiveRandomSprites();

        float randomScaler = Random.Range(-GameManager.GameSettings.NPCScaleVariance, GameManager.GameSettings.NPCScaleVariance);
        transform.localScale = (Vector3)defaultScale + new Vector3(randomScaler, randomScaler, randomScaler);
    }

}

public class RandomNPCLooks
{
    public NPCVisualLook NeutralLook;
    public NPCVisualLook HappyLook;
    public NPCVisualLook AngryLook;
    public Sprite BaseHead;
    public Sprite Body;
}

public class NPCVisualLook
{
    public Sprite Eyes;    
    public Sprite Nose;
    public Sprite Mouth;
}

public enum NPCMood
{
    Neutral,
    Happy,
    Angry
}