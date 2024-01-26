using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] private float lookSpeed = 3;
    [SerializeField, Range(0f, 90)] private float verticalClamp = 89f;
    [SerializeField, Range(0f, 180)] private float horizontalClamp = 90f;

    private Vector2 rotation;

    public void RotateLook(Vector2 rotationDelta) // Look rotation (UP down is Camera) (Left right is Transform rotation)
    {
        rotation.y += rotationDelta.x * lookSpeed;
        rotation.x += -rotationDelta.y * lookSpeed;
        rotation.y = Mathf.Clamp(rotation.y, -horizontalClamp, horizontalClamp);
        rotation.x = Mathf.Clamp(rotation.x, -verticalClamp, verticalClamp);

        //transform.eulerAngles = new Vector2(0, rotation.y);
        transform.localRotation = Quaternion.Euler(rotation.x, rotation.y, 0);
    }
}
