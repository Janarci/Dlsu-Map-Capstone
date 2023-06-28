using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMove : MonoBehaviour
{
    private JoystickController joystick;

    private void Start()
    {
        joystick = FindObjectOfType<JoystickController>();
        joystick.OnJoystickMove += HandleJoystickMove;
    }

    private void HandleJoystickMove(Vector2 direction)
    {
        Debug.Log(direction.x + " " + direction.y);
        // Use the direction vector to control the player movement
        // Example: transform.Translate(direction.x, 0f, direction.y);
    }

    private void OnDestroy()
    {
        joystick.OnJoystickMove -= HandleJoystickMove;
    }
}
