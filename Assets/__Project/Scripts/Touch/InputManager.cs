using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private ControllerInput touchControl;

    public delegate void StartTouchEvent(bool value);
    public event StartTouchEvent OnStartTouch;

    public delegate void EndTouchEvent(bool value);
    public event EndTouchEvent OnEndTouch;

    public delegate void UpdateTouchEvent(Vector2 position);
    public event UpdateTouchEvent OnUpdateTouch;

    private void Awake()
    {
        touchControl = new ControllerInput();
    }

    private void OnEnable()
    {
        touchControl.Enable();
    }
    private void OnDisable()
    {
        touchControl.Disable();
    }

    private void FixedUpdate()
    {
        if (OnUpdateTouch != null)
            OnUpdateTouch(touchControl.Touch.TouchPosition.ReadValue<Vector2>());
    }
    private void Start()
    {
        touchControl.Touch.TouchPress.started += ctx => StartTouch(ctx);
        touchControl.Touch.TouchPress.canceled += ctx => EndTouch(ctx);
    }

    private void EndTouch(InputAction.CallbackContext context)
    {
        if (OnEndTouch != null) OnEndTouch(false);
    }

    private void StartTouch(InputAction.CallbackContext context)
    {
        if (OnStartTouch != null) OnStartTouch(true);
    }
}
