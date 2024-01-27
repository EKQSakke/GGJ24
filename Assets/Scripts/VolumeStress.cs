using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(Volume))]
public class VolumeStress : MonoBehaviour
{
    [SerializeField] private AnimationCurve volumeWeightCurve;

    private Volume volume;

    void Awake()
    {
        volume = GetComponent<Volume>();
    }

    void Update()
    {
        if (GameManager.Instance == null)
            return;

        volume.weight = volumeWeightCurve.Evaluate(GameManager.Instance.Stress);
    }
}
