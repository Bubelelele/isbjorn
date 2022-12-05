using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerInput : MonoBehaviour
{
        private InputActions _input;
        public Vector2 MoveInput { get; private set; }
        public bool MoveIsPressed { get; private set; }
        public bool RunIsPressed { get; private set; }
        public bool JumpWasPressed { get; set; }
        public bool JumpIsHeld { get; private set; }
        public bool RollIsPressed { get; private set; }
        public bool Roaring { get; private set; }
        public bool Slashing { get; private set; }
        public bool Sniffing { get; private set; }

        private void OnEnable()
        {
            _input = new InputActions();
            _input.PlayerLand.Enable();

            _input.PlayerLand.Move.started += StoreMoveInput;
            _input.PlayerLand.Move.performed += StoreMoveInput;
            _input.PlayerLand.Move.canceled += StoreMoveInput;
            
            _input.PlayerLand.Run.started += SetRun;
            _input.PlayerLand.Run.canceled += SetRun;
            
            _input.PlayerLand.Jump.started += SetJump;
            _input.PlayerLand.Jump.canceled += SetJump;
            
            _input.PlayerLand.Roll.started += SetRoll;
            _input.PlayerLand.Roll.canceled += SetRoll;
            
            _input.PlayerLand.Slash.started += Slash;
            _input.PlayerLand.Slash.canceled += Slash;
            
            _input.PlayerLand.Roar.started += Roar;
            _input.PlayerLand.Roar.canceled += Roar;

            _input.PlayerLand.Sniff.started += Sniff;
            _input.PlayerLand.Sniff.canceled += Sniff;
        }

        private void Update()
        {
            CursorLockToggle();
        }

        private void OnDisable()
        {
            _input.PlayerLand.Move.started -= StoreMoveInput;
            _input.PlayerLand.Move.performed -= StoreMoveInput;
            _input.PlayerLand.Move.canceled -= StoreMoveInput;
            
            _input.PlayerLand.Run.started -= SetRun;
            _input.PlayerLand.Run.canceled -= SetRun;
            
            _input.PlayerLand.Jump.started -= SetJump;
            _input.PlayerLand.Jump.canceled -= SetJump;
            
            _input.PlayerLand.Roll.started -= SetRoll;
            _input.PlayerLand.Roll.canceled -= SetRoll;
            
            _input.PlayerLand.Slash.started -= Slash;
            _input.PlayerLand.Slash.canceled -= Slash;
            
            _input.PlayerLand.Roar.started -= Roar;
            _input.PlayerLand.Roar.canceled -= Roar;

            _input.PlayerLand.Sniff.started -= Sniff;
            _input.PlayerLand.Sniff.canceled -= Sniff;

            _input.PlayerLand.Disable();
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
            JumpWasPressed = JumpIsHeld = context.started;
        }
        
        private void SetRoll(InputAction.CallbackContext context)
        {
            RollIsPressed = context.started;
        }

        private void Slash(InputAction.CallbackContext context)
        {
            Slashing = context.started;
        }
        
        private void Roar(InputAction.CallbackContext context)
        {
            Roaring = context.started;
        }

        private void Sniff(InputAction.CallbackContext context)
        {
            Sniffing = context.started;
        }
        
        private void CursorLockToggle()
        {
            if (SceneManager.GetActiveScene().buildIndex == 0) return;
            
            switch (Cursor.lockState == CursorLockMode.Locked)
            {
                case true when Keyboard.current.escapeKey.wasPressedThisFrame:
                    Cursor.lockState = CursorLockMode.None;
                    break;
                case false when Mouse.current.leftButton.wasPressedThisFrame:
                    Invoke(nameof(LockCursor), 0.2f);
                    break;
            }
        }
        
        private void LockCursor()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
}