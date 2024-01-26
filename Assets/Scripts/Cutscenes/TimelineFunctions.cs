using UnityEngine;

public class TimelineFunctions : MonoBehaviour
{
    public void TestMe()
    {
        Debug.Log("TestMe");
    }

    public void EndCutscene()
    {
        CutsceneManager.instance.ReturnToLastScene();
    }
}