using UnityEngine;

public class TimelineFunctions : MonoBehaviour
{
    public void TestMe()
    {
        DialogueDrawer.Instance.ShowText("Lorem ipsum dolor sit amet, consectetur adipiscing elit. Suspendisse volutpat id ante sed luctus. Nunc quis enim nec velit efficitur convallis. Donec tincidunt condimentum varius. Aliquam non ipsum sit amet arcu rutrum vulputate. Fusce vel venenatis enim, at posuere elit. Pellentesque vel ipsum dolor. Integer felis ligula, faucibus a nulla at, sollicitudin tempus turpis.");
    }

    public void EndCutscene()
    {
        CutsceneManager.Instance.ReturnToLastScene();
    }
}