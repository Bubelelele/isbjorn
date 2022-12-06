using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerInput : MonoBehaviour
{
        public InputActions InputActions;
        public Vector2 MoveInput { get; private set; }
        public bool Moving { get; private set; }
        public bool Running { get; private set; }
        public bool JumpWasPressed { get; set; }
        public bool JumpIsHeld { get; private set; }
        public bool Rolling { get; private set; }
        public bool Roaring { get; private set; }
        public bool Slashing { get; private set; }
        public bool Sniffing { get; private set; }

        private void OnEnable()
        {
            InputActions = new InputActions();
            InputActions.PlayerLand.Enable();

            InputActions.PlayerLand.Move.started += StoreMoveInput;
            InputActions.PlayerLand.Move.performed += StoreMoveInput;
            InputActions.PlayerLand.Move.canceled += StoreMoveInput;
            
            InputActions.PlayerLand.Run.started += Run;
            InputActions.PlayerLand.Run.canceled += Run;
            
            InputActions.PlayerLand.Jump.started += Jump;
            InputActions.PlayerLand.Jump.canceled += Jump;
            
            InputActions.PlayerLand.Roll.started += Roll;
            InputActions.PlayerLand.Roll.canceled += Roll;
            
            InputActions.PlayerLand.Slash.started += Slash;
            InputActions.PlayerLand.Slash.canceled += Slash;
            
            InputActions.PlayerLand.Roar.started += Roar;
            InputActions.PlayerLand.Roar.canceled += Roar;

            InputActions.PlayerLand.Sniff.started += Sniff;
            InputActions.PlayerLand.Sniff.canceled += Sniff;
        }

        private void Update()
        {
            CursorLockToggle();
        }

        private void OnDisable()
        {
            InputActions.PlayerLand.Move.started -= StoreMoveInput;
            InputActions.PlayerLand.Move.performed -= StoreMoveInput;
            InputActions.PlayerLand.Move.canceled -= StoreMoveInput;
            
            InputActions.PlayerLand.Run.started -= Run;
            InputActions.PlayerLand.Run.canceled -= Run;
            
            InputActions.PlayerLand.Jump.started -= Jump;
            InputActions.PlayerLand.Jump.canceled -= Jump;
            
            InputActions.PlayerLand.Roll.started -= Roll;
            InputActions.PlayerLand.Roll.canceled -= Roll;
            
            InputActions.PlayerLand.Slash.started -= Slash;
            InputActions.PlayerLand.Slash.canceled -= Slash;
            
            InputActions.PlayerLand.Roar.started -= Roar;
            InputActions.PlayerLand.Roar.canceled -= Roar;

            InputActions.PlayerLand.Sniff.started -= Sniff;
            InputActions.PlayerLand.Sniff.canceled -= Sniff;

            InputActions.PlayerLand.Disable();
        }
        
        private void StoreMoveInput(InputAction.CallbackContext context)
        {
            MoveInput = context.ReadValue<Vector2>();
            Moving = MoveInput != Vector2.zero;
        }

        private void Run(InputAction.CallbackContext context)
        {
            Running = context.started;
        }
        
        private void Jump(InputAction.CallbackContext context)
        {
            JumpWasPressed = JumpIsHeld = context.started;
        }
        
        private void Roll(InputAction.CallbackContext context)
        {
            Rolling = context.started;
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