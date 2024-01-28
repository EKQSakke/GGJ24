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

    private NPCMood currentMood = NPCMood.Neutral;
    private float timeToBlink;
    private float timeSinceLastBlink = 0f;
    private bool blinking = false;

    private void Update()
    {
        if (myLook != null)
        {
            timeSinceLastBlink += Time.deltaTime;

            if (timeSinceLastBlink >= timeToBlink)
            {
                timeSinceLastBlink = 0f;

                if (blinking)
                {
                    timeToBlink = Random.Range(6f, 30f);
                }
                else
                {
                    timeToBlink = Random.Range(0.1f, 0.3f);
                    //timeToBlink = Random.Range(1f, 2f);
                }

                blinking = !blinking;
                UpdateEyes();
            }            
        }
    }

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

        timeSinceLastBlink = 0f;
        timeToBlink = Random.Range(6f, 30f);
        blinking = false;
    }

    public void SetMood(NPCMood mood)
    {
        currentMood = mood;

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
        myLook.ClosedEyes = GameManager.GameSettings.ClosedEyes.GetRandomElementFromList();

        myLook.NeutralLook = GameManager.GameSettings.NeutralSprites.GiveRandomSprites();
        myLook.AngryLook = GameManager.GameSettings.AngrySprites.GiveRandomSprites();
        myLook.HappyLook = GameManager.GameSettings.HappySprites.GiveRandomSprites();

        float randomScaler = Random.Range(-GameManager.GameSettings.NPCScaleVariance, GameManager.GameSettings.NPCScaleVariance);
        transform.localScale = (Vector3)defaultScale + new Vector3(randomScaler, randomScaler, randomScaler);
    }

    private void UpdateEyes()
    {
        if (blinking)
        {
            EyesRenderer.sprite = myLook.ClosedEyes;
            return;
        }

        switch (currentMood)
        {
            case NPCMood.Happy:
                EyesRenderer.sprite = myLook.HappyLook.Eyes;
                break;
            case NPCMood.Angry:
                EyesRenderer.sprite = myLook.AngryLook.Eyes;
                break;
            default:
                EyesRenderer.sprite = myLook.NeutralLook.Eyes;
                break;
        }
    }

}

public class RandomNPCLooks
{
    public NPCVisualLook NeutralLook;
    public NPCVisualLook HappyLook;
    public NPCVisualLook AngryLook;
    public Sprite BaseHead;
    public Sprite Body;
    public Sprite ClosedEyes;
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