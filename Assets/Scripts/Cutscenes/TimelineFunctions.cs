using UnityEngine;

public class TimelineFunctions : MonoBehaviour
{
    public void TestMe()
    {
        DialogueDrawer.Instance.ShowText("Lorem ipsum dolor sit amet.");
    }

    public void EndCutscene()
    {
        CutsceneManager.Instance.ReturnToLastScene();
    }
}