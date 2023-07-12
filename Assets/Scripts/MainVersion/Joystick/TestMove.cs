using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMove : MonoBehaviour
{
    public JoystickController joystick;
    public Transform cameraTransform;
    Vector3 direction;

    int speed = 5;
    private void Start()
    {
        //joystick = FindObjectOfType<JoystickController>();
        joystick.OnJoystickMove += HandleJoystickMove;
    }

	private void Update()
	{
        Debug.Log(direction.x + " " + direction.y);

        // Calculate the movement vector based on the joystick direction
        Vector3 movement = new Vector3(direction.x, 0f, direction.y);

        // Transform the movement vector to align with the player's local space
        //movement = transform.TransformDirection(movement);

        Quaternion cameraRotation = Quaternion.Euler(0f, cameraTransform.eulerAngles.y, 0f);
        movement = cameraRotation * movement;

        // Move the player based on the transformed movement vector
        transform.Translate(movement * speed * Time.deltaTime, Space.World);

    }
    private void HandleJoystickMove(Vector3 joystickdirection)
    {
        direction = joystickdirection;
    }

    private void OnDestroy()
    {
        joystick.OnJoystickMove -= HandleJoystickMove;
    }
}
