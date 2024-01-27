using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    DialogueDrawer dialogueDrawer;
    // Start is called before the first frame update
    void Start()
    {
        dialogueDrawer = DialogueDrawer.Instance;
        gameObject.SetActive(false);
    }

    IEnumerator BossAngry()
    {
        
        dialogueDrawer.ShowText("Boss: You stupid cunt!");
        yield return new WaitForSeconds(5);
        gameObject.SetActive(false);
        
    }

    IEnumerator BossHappy()
    {
        dialogueDrawer.ShowText("Boss: Brilliant! You really showed that fucker who's the Boss.");
        yield return new WaitForSeconds(5);
        gameObject.SetActive(false);

    }

    //Call this one
    public void TriggerBossDialogue(bool bossHappy)
    {
        gameObject.SetActive(true);
        if (bossHappy)
        {
            StartCoroutine(BossHappy());
            return;
        }

        StartCoroutine(BossAngry());
    }
}
