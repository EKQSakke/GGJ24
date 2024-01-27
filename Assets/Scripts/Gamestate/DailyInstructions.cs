using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DailyInstructions : MonoBehaviour
{
    TMPro.TMP_Text text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponentInChildren<TMP_Text>(true);
        GameManager.RoundStart = delegate () { UpdateText(); };
    }

    void UpdateText()
    {
        GameManager gameManager = GameManager.Instance;
        DayRules dayRules = gameManager.GameRounds[gameManager.CurrentGameRound].DayRules;
        text.text = "Give the people what ever they want\n\n ONLY if their...";

        if (dayRules.City != "" )
        {
            text.text += "\n City is <color=orange>" + dayRules.City + "</color>";
        }
        if (dayRules.Occupation != "" )
        {
            text.text += "\n Occupation is <color=orange>" + dayRules.Occupation + "</color>";
        }
        if (dayRules.itemNeeded != UsableItemType.None )
        {
            text.text += "\n Customer request is <color=orange>" + dayRules.itemNeeded.ToString() + "</color>";
        }
    }
}
