using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Player), typeof(PlayerInput))]
public class PlayerInputController : MonoBehaviour
{
    private Player player;

    void Awake()
    {
        Initialize();
    }

    void Initialize()
    {
        player = GetComponent<Player>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void OnLook(InputValue inputValue)
    {
        Vector2 value = inputValue.Get<Vector2>();
        Debug.Log(value);
        player.Look.RotateLook(value);
    }
}