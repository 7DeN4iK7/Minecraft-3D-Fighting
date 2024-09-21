using System;
using InputSystem.KeyboardNMouse;
using UnityEngine;

namespace InputSystem
{
    public enum InputType
    {
        Player1,
        Player2,
        AI
    }
    
    public class GameplayInput : MonoBehaviour
    {
        [SerializeField] private InputType inputType;

        private InputPlayer1 _player1;
        //private InputPlayer2 _player2;

        public Action<float> MovePerformed;
        public Action UseButtonPressed;
        public Action UseButtonCanceled;
        
        public Action MoveButtonStarted;
        public Action MoveButtonCanceled;
        
        public Action DownButtonStarted;
        public Action DownButtonCanceled;

        public Action UpButtonStarted;
        public Action UpButtonCanceled;

        public Action JumpPressed;

        /*private void Awake()
        {
            _inputMap = new PlayerInputMap();
            _inputMap.Enable();
        
            InitKeyboardAndMouse();
        }

        private void InitKeyboardAndMouse()
        {
            _kamInput = new InputPlayer1(_inputMap);
            _kamInput.HorizontalMovePerformed += MovePerformed;
            _kamInput.UseButtonPressed += UseButtonPressed;
            _kamInput.UseButtonCanceled += UseButtonCanceled;
            
            _kamInput.MoveButtonStarted += MoveButtonStarted;
            _kamInput.MoveButtonCanceled += MoveButtonCanceled;
            
            _kamInput.DownButtonStarted += DownButtonStarted;
            _kamInput.DownButtonCanceled += DownButtonCanceled;

            _kamInput.JumpPressed += JumpPressed;
        }*/
    }

}
