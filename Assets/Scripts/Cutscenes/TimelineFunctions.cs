using UnityEngine;

public class TimelineFunctions : MonoBehaviour
{
    public void TestMe()
    {
        DialogueDrawer.Instance.ShowText("How relaxing...");
    }

    public void EndCutscene()
    {
        CutsceneManager.Instance.ReturnToGame();
    }
}