using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class SteveInput : MonoBehaviour
{
    public enum SteveInputType
    {
        Player1,
        Player2,
        Gamepad,
        AI
    }
    
    [SerializeField] private SteveInputType inputType;

    public Action<float> MoveStarted;
    public Action<float> MoveCanceled;
    public Action<float> MovePerformed;

    public Action JumpPressed;
    public Action JumpCanceled;

    public Action UseButtonPressed;
    public Action UseButtonCanceled;

    public Action DownPressed;
    public Action DownCanceled;

    public Action UpPressed;
    public Action UpCanceled;

    public Action AttackButtonPressed;
    public Action AttackButtonReleased;
    
    private PlayerInput _input;
    
    public bool JumpIsPressed { get; private set; }
    public bool DownIsPressed { get; private set; }
    public bool UseIsPressed { get; private set; }
    public bool UpIsPressed { get; private set; }
    public bool AttackIsPressed { get; private set; }

    public float MoveValue { get; private set; }

    private void Awake()
    {
        _input = GetComponent<PlayerInput>();
        switch (inputType)
        {
            case SteveInputType.Player1:
                _input.SwitchCurrentControlScheme("Keyboard1", Keyboard.current);
                break;
            case SteveInputType.Player2:
                _input.SwitchCurrentControlScheme("Keyboard2", Keyboard.current);
                break;
            case SteveInputType.Gamepad:
                _input.SwitchCurrentControlScheme("Gamepad", Gamepad.current);
                break;
        }
    }

    private void Start()
    {
        _input.actions["HorizontalMove"].started += OnMoveStarted;
        _input.actions["HorizontalMove"].canceled += OnMoveCanceled;
        _input.actions["HorizontalMove"].performed += OnMovePerformed;

        _input.actions["Jump"].started += OnJumpPressed;
        _input.actions["Jump"].canceled += OnJumpCanceled;

        _input.actions["Use"].started += OnUseStarted;
        _input.actions["Use"].canceled += OnUseCanceled;

        _input.actions["Down"].started += OnDownStarted;
        _input.actions["Down"].canceled += OnDownCanceled;

        _input.actions["Up"].started += OnUpStarted;
        _input.actions["Up"].canceled += OnUpCanceled;
    }
    
    private void OnDestroy()
    {
        _input.actions["HorizontalMove"].started -= OnMoveStarted;
        _input.actions["HorizontalMove"].canceled -= OnMoveCanceled;
        _input.actions["HorizontalMove"].performed -= OnMovePerformed;
        
        _input.actions["Jump"].started -= OnJumpPressed;
        _input.actions["Jump"].canceled -= OnJumpCanceled;

        _input.actions["Use"].started -= OnUseStarted;
        _input.actions["Use"].canceled -= OnUseCanceled;

        _input.actions["Down"].started -= OnDownStarted;
        _input.actions["Down"].canceled -= OnDownCanceled;

        _input.actions["Up"].started -= OnUpStarted;
        _input.actions["Up"].canceled -= OnUpCanceled;
    }

    private void OnMoveStarted(InputAction.CallbackContext obj)
    {
        float value = obj.ReadValue<float>();
        MoveValue = value;

        MoveStarted?.Invoke(value);
    }
    
    private void OnMoveCanceled(InputAction.CallbackContext obj)
    {
        MoveValue = 0;

        MoveCanceled?.Invoke(obj.ReadValue<float>());
    }
    
    private void OnMovePerformed(InputAction.CallbackContext obj)
    {
        float value = obj.ReadValue<float>();
        MoveValue = value;

        MovePerformed?.Invoke(value);
    }
    
    private void OnJumpPressed(InputAction.CallbackContext obj)
    {
        JumpIsPressed = true;
        JumpPressed?.Invoke();
    }
    private void OnJumpCanceled(InputAction.CallbackContext obj)
    {
        JumpIsPressed = false;
        JumpCanceled?.Invoke();
    }
    
    private void OnUseStarted(InputAction.CallbackContext obj)
    {
        UseIsPressed = true;
        UseButtonPressed?.Invoke();
    }
    private void OnUseCanceled(InputAction.CallbackContext obj)
    {
        UseIsPressed = false;
        UseButtonCanceled?.Invoke();
    }
    
    private void OnDownStarted(InputAction.CallbackContext obj)
    {
        DownIsPressed = true;
        DownPressed?.Invoke();
    }
    private void OnDownCanceled(InputAction.CallbackContext obj)
    {
        DownIsPressed = false;
        DownCanceled?.Invoke();
    }
    
    private void OnUpStarted(InputAction.CallbackContext obj)
    {
        UpIsPressed = true;
        UpPressed?.Invoke();
    }
    private void OnUpCanceled(InputAction.CallbackContext obj)
    {
        UpIsPressed = false;
        UpCanceled?.Invoke();
    }
    
    
    
}
