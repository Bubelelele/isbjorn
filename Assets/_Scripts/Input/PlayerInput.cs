using UnityEngine;
using UnityEngine.InputSystem;

namespace _Scripts.Input
{
    public class PlayerInput : MonoBehaviour
    {
        public Vector2 MoveInput { get; private set; }
        public bool MoveIsPressed { get; private set; }
        public bool RunIsPressed { get; private set; }
        public bool JumpIsPressed { get; private set; }
        public bool RollIsPressed { get; private set; }
        public bool Roaring { get; private set; }
        public bool Slashing { get; private set; }
        public bool Smelling { get; private set; }

        private InputActions _input;
        
        private void OnEnable()
        {
            _input = new InputActions();
            _input.Player.Enable();

            _input.Player.Move.performed += StoreMoveInput;
            _input.Player.Move.canceled += StoreMoveInput;
            
            _input.Player.Run.started += SetRun;
            _input.Player.Run.canceled += SetRun;
            
            _input.Player.Jump.started += SetJump;
            _input.Player.Jump.canceled += SetJump;
            
            _input.Player.Roll.started += SetRoll;
            _input.Player.Roll.canceled += SetRoll;
            
            _input.Player.Roar.started += Roar;
            _input.Player.Roar.canceled += Roar;
            
            _input.Player.Slash.started += Slash;
            _input.Player.Slash.canceled += Slash;
            
            _input.Player.Smell.started += Smell;
            _input.Player.Smell.canceled += Smell;
        }
        
        private void OnDisable()
        {
            _input.Player.Move.performed -= StoreMoveInput;
            _input.Player.Move.canceled -= StoreMoveInput;
            
            _input.Player.Run.started -= SetRun;
            _input.Player.Run.canceled -= SetRun;
            
            _input.Player.Jump.started -= SetJump;
            _input.Player.Jump.canceled -= SetJump;
            
            _input.Player.Roll.started -= SetRoll;
            _input.Player.Roll.canceled -= SetRoll;
            
            _input.Player.Roar.started -= Roar;
            _input.Player.Roar.canceled -= Roar;
            
            _input.Player.Slash.started -= Slash;
            _input.Player.Slash.canceled -= Slash;
            
            _input.Player.Smell.started -= Smell;
            _input.Player.Smell.canceled -= Smell;

            _input.Player.Disable();
        }
        
        private void StoreMoveInput(InputAction.CallbackContext context)
        {
            MoveInput = context.ReadValue<Vector2>();
            MoveIsPressed = MoveInput != Vector2.zero;

        }

        private void SetRun(InputAction.CallbackContext context)
        {
            RunIsPressed = context.started;
        }
        
        private void SetJump(InputAction.CallbackContext context)
        {
            JumpIsPressed = context.started;
        }
        
        private void SetRoll(InputAction.CallbackContext context)
        {
            RollIsPressed = context.started;
        }

        private void Roar(InputAction.CallbackContext context)
        {
            Roaring = context.started;
        }
        
        private void Slash(InputAction.CallbackContext context)
        {
            Slashing = context.started;
        }
        
        private void Smell(InputAction.CallbackContext context)
        {
            Smelling = context.started;
        }
    }
}
