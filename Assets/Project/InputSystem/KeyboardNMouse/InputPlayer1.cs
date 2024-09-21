using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace InputSystem.KeyboardNMouse
{
    public class InputPlayer1
    {
        /*private Coroutine _moveSenderRoutine;
        private PlayerInputMap _inputMap;
    
        public Action<float> HorizontalMovePerformed;
        
        public Action UseButtonPressed;
        public Action UseButtonCanceled;
        
        
        public Action MoveButtonStarted;
        public Action MoveButtonCanceled;
        
        public Action DownButtonStarted;
        public Action DownButtonCanceled;
        
        public Action UpButtonStarted;
        public Action UpButtonCanceled;

        public Action JumpPressed;
        
        public InputPlayer1(PlayerInputMap inputMap)
        {
            _inputMap = inputMap;
            /*_inputMap.KeyboardAndMouse.HorizontalMove.started += OnMoveStarted;
            _inputMap.KeyboardAndMouse.HorizontalMove.canceled += OnMoveCanceled;
            _inputMap.KeyboardAndMouse.HorizontalMove.performed += OnMovePerformed;
            
            _inputMap.KeyboardAndMouse.Use.started += OnUseButtonPressed;
            _inputMap.KeyboardAndMouse.Use.canceled += OnUseButtonCanceled;
            
            _inputMap.KeyboardAndMouse.Down.started += OnDownButtonStarted;
            _inputMap.KeyboardAndMouse.Down.canceled += OnDownButtonCanceled;

            _inputMap.KeyboardAndMouse.Up.started += OnUpButtonPressed;
            _inputMap.KeyboardAndMouse.Up.canceled += OnUpButtonCanceled;

            _inputMap.KeyboardAndMouse.Jump.performed += OnJumpButtonPressed;#1#
        }

        private void OnMovePerformed(InputAction.CallbackContext obj)
        {
            HorizontalMovePerformed?.Invoke(obj.ReadValue<float>());
        }

        public void Unsubscribe()
        {
            /*_inputMap.KeyboardAndMouse.HorizontalMove.started -= OnMoveStarted;
            _inputMap.KeyboardAndMouse.HorizontalMove.canceled -= OnMoveCanceled;
            
            _inputMap.KeyboardAndMouse.Use.started -= OnUseButtonPressed;
            _inputMap.KeyboardAndMouse.Use.canceled -= OnUseButtonCanceled;
            
            _inputMap.KeyboardAndMouse.Down.started -= OnDownButtonStarted;
            _inputMap.KeyboardAndMouse.Down.canceled -= OnDownButtonCanceled;

            _inputMap.KeyboardAndMouse.Up.started -= OnUpButtonPressed;
            _inputMap.KeyboardAndMouse.Up.canceled -= OnUpButtonCanceled;
            
            _inputMap.KeyboardAndMouse.Jump.performed -= OnJumpButtonPressed;#1#

            Coroutines.TryStopRoutine(_moveSenderRoutine);
        }

        private void OnJumpButtonPressed(InputAction.CallbackContext obj)
        {
            JumpPressed?.Invoke();
        }
        

        private void OnUpButtonPressed(InputAction.CallbackContext obj)
        {
            UpButtonStarted?.Invoke();
        }
        
        private void OnUpButtonCanceled(InputAction.CallbackContext obj)
        {
            UpButtonCanceled?.Invoke();
        }

        private void OnDownButtonCanceled(InputAction.CallbackContext obj)
        {
            DownButtonCanceled?.Invoke();
        }

        private void OnDownButtonStarted(InputAction.CallbackContext obj)
        {
            DownButtonStarted?.Invoke();
        }

        private void OnUseButtonCanceled(InputAction.CallbackContext context)
        {
            UseButtonCanceled?.Invoke();
        }
        
        private void OnUseButtonPressed(InputAction.CallbackContext context)
        {
            UseButtonPressed?.Invoke();
        }
        private void OnMoveStarted(InputAction.CallbackContext context)
        {
            MoveButtonStarted?.Invoke();
        }
    
        private void OnMoveCanceled(InputAction.CallbackContext context)
        {
            HorizontalMovePerformed?.Invoke(context.ReadValue<float>());
            MoveButtonCanceled?.Invoke();
        }*/
    }
}
