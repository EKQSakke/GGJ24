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
        if (GameManager.Instance != null && GameManager.Instance.GameCurrentlyActive == false)
            return;

        Vector2 value = inputValue.Get<Vector2>();
        player.Look.RotateLook(value);
    }

    void OnInteract(InputValue inputValue)
    {
        if (GameManager.Instance != null && GameManager.Instance.GameCurrentlyActive == false)
            return;

        float value = inputValue.Get<float>();

        if (value == 1f)
            player.Interactor.StartInteraction();
        else
            player.Interactor.EndInteraction();
    }

}