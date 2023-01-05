using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Jam
{
    [RequireComponent(typeof(CharacterMover))]
    public class Player : MonoBehaviour
    {
        // Private Implementation
        private CharacterMover _mover;

        private Vector2 _moveInput;
        private bool _holdUpInput;
        private bool _throwInput;

        private void Awake()
        {
            _mover = GetComponent<CharacterMover>();
        }

        private void Update()
        {
            _mover.Move(_moveInput);
        }

        public void OnMove(InputAction.CallbackContext callback)
        {
            _moveInput = callback.ReadValue<Vector2>();
        }

        public void OnHoldUp(InputAction.CallbackContext callback)
        {
            _holdUpInput = callback.performed;
        }

        public void OnJump(InputAction.CallbackContext callback)
        {
            if(callback.started)
            {
                _mover.Jump();
            }
        }

        public void OnThrow(InputAction.CallbackContext callback)
        {
            _throwInput = callback.started;
        }
    }
}
