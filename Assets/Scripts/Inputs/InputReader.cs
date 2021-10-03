using System;
using UnityEngine;
using UnityEngine.InputSystem;

[DisallowMultipleComponent]
public class InputReader : MonoBehaviour
{
    public static Vector2 MoveInput { get; private set; }
    public static Vector2 RotateInput { get; private set; }

    ///<summary>
    /// Too much trouble with boolean variables:
    /// started,perfomed,cancel interaction that called in event so I can't properly set bool to button type
    /// In the Unity repo (starter assets), they made a strange decision making public variable inside their input reader and setting it to false in other script. 
    /// So I considered to wrap up events with my own static events.
    ///</summary>

    public static event Action IsJumpPressedAction;

    public static event Action IsGrabPressedAction;

    private PlayerInputs _inputAsset;

    private void Awake()
    {
        _inputAsset = new PlayerInputs();

        _inputAsset.Movement.Move.performed += OnMove;
        _inputAsset.Movement.Move.canceled += OnMove;

        _inputAsset.Movement.Rotate.performed += OnRotate;
        _inputAsset.Movement.Rotate.canceled += OnRotate;

        _inputAsset.Movement.Jump.performed += OnJump;

        _inputAsset.Movement.Intearct.performed += OnIntearct;

        LockCursor(true);
    }

    private void OnEnable()
    {
        _inputAsset.Enable();
    }

    private void OnDisable()
    {
        _inputAsset.Disable();
    }

    private void OnMove(InputAction.CallbackContext ctx)
    {
        MoveInput = ctx.ReadValue<Vector2>();
    }

    private void OnRotate(InputAction.CallbackContext ctx)
    {
        RotateInput = ctx.ReadValue<Vector2>();
    }

    private void OnIntearct(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            IsGrabPressedAction.Invoke();
        }
    }

    private void OnJump(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            IsJumpPressedAction.Invoke();
        }
    }

    private void LockCursor(bool state)
    {
        Cursor.lockState = state ? CursorLockMode.Locked : CursorLockMode.None;
    }
}
