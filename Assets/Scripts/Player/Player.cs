using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerLook look; public PlayerLook Look { get { return look; } }
    [SerializeField] private Interactor interactor; public Interactor Interactor { get { return interactor; } }
}
