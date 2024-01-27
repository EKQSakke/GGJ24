using Cinemachine;
using UnityEngine;

public class CameraStress : MonoBehaviour
{
    CinemachineVirtualCamera cam;
    CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin;

    [SerializeField]
    [Range(0.0f, 5.0f)]
    float shakeMultiplier = 1;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        cinemachineBasicMultiChannelPerlin = GetComponent<CinemachineVirtualCamera>()
            .GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance == null)
            return;

        var stress = GameManager.Instance.Stress;
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = stress * shakeMultiplier * .2f;
        cinemachineBasicMultiChannelPerlin.m_FrequencyGain = stress * shakeMultiplier * 2;
    }
}
