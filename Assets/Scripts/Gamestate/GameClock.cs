using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameClock : MonoBehaviour
{
    public Material ClockMaterial;
    
    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.GameCurrentlyActive)
        {
            float valueTranslated = Mathf.Lerp(-1, 1, GameManager.Instance.RoundTimePercent);
            ClockMaterial.SetFloat("_FillRate", valueTranslated);
        }
    }

}
