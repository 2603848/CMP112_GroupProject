using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;
using UnityEngine.Windows;

public class PlayerController : MonoBehaviour
{
    CharacterController characterController;

    InputAction MoveAction;
    PlayerInput playerInput;

    public float Speed = 5;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();

        MoveAction = playerInput.actions.FindAction("Move");

        Mouse.current.WarpCursorPosition(new Vector2(Screen.width/2.0f,Screen.height/2.0f));
    }

    // Performs rotation based on the current mouse position
    void MouseRotation()
    {
        Vector2 mouseMovement = Mouse.current.position.value - new Vector2(Screen.width / 2.0f, Screen.height / 2.0f);
        transform.Rotate(Vector3.up, mouseMovement.x * 10.0f * Time.deltaTime);
        Mouse.current.WarpCursorPosition(new Vector2(Screen.width / 2.0f, Screen.height / 2.0f));
    }
    // Moves the player based on their rotation and inputs
    void PlayerMovement()
    {
        Vector2 moveValue = MoveAction.ReadValue<Vector2>() * Speed;
        float yaw = transform.eulerAngles.y * Mathf.Deg2Rad;

        Vector3 rotated = new Vector3(0, 0, 0);

        rotated.x = (moveValue.x * Mathf.Cos(yaw) + moveValue.y * Mathf.Sin(yaw)) * Time.deltaTime;
        rotated.z = (-moveValue.x * Mathf.Sin(yaw) + moveValue.y * Mathf.Cos(yaw)) * Time.deltaTime;

        characterController.Move(rotated);
        characterController.Move(new Vector3(0, -9.8f * Time.deltaTime, 0));
    }

    // Update is called once per frame
    void Update()
    {
        MouseRotation();
        PlayerMovement();
    }
}
