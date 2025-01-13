using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    private InputAction _moveAction;

    public static event Action<Vector2> OnMoveEvent;

    private void OnEnable()
    {
        _moveAction = GetComponent<PlayerInput>().actions["Move"];
        _moveAction.performed += OnMove;
        _moveAction.Enable();
    }

    private void OnDisable()
    {
        _moveAction.performed -= OnMove;
        _moveAction.Disable();
    }



    //Gets fired when _moveAction is pressed
    private void OnMove(InputAction.CallbackContext context)
    {
        Vector2 moveInput = context.ReadValue<Vector2>();
        OnMoveEvent?.Invoke(moveInput);
    }
}
