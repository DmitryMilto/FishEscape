using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private ControllerInput touchControl;

    public delegate void StartTouchEvent(Vector2 position, float time);
    public event StartTouchEvent OnStartTouch;

    public delegate void EndTouchEvent(Vector2 position, float time);
    public event EndTouchEvent OnEndTouch;

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

    private void Start()
    {
        touchControl.Touch.TouchPress.started += ctx => StartTouch(ctx);
        touchControl.Touch.TouchPress.canceled += ctx => EndTouch(ctx);
    }

    private void EndTouch(InputAction.CallbackContext context)
    {
        if (OnEndTouch != null) OnEndTouch(touchControl.Touch.TouchPosition.ReadValue<Vector2>(), (float)context.time);
    }

    private void StartTouch(InputAction.CallbackContext context)
    {
        if (OnStartTouch != null) OnStartTouch(touchControl.Touch.TouchPosition.ReadValue<Vector2>(), (float)context.startTime);
    }
}
