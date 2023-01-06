using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Jam
{
    [RequireComponent(typeof(CharacterMover))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class Player : MonoBehaviour
    {
        // Private Implementation
        private CharacterMover _mover;
        private Umbrella _umbrella;
        private UmbrellaSensor _sensor;

        private Vector2 _moveInput;
        private bool _holdUpInput;
        private bool _throwInput;

        private void Awake()
        {
            _mover = GetComponent<CharacterMover>();
            _umbrella = GetComponentInChildren<Umbrella>();
            _sensor = GetComponentInChildren<UmbrellaSensor>();

            Debug.Assert(_sensor != null, $"{name} cannot find MovementSensor component in children.");
            Debug.Assert(_umbrella != null, $"{name} cannot find Umbrella component in children.");

            _umbrella.Setup(GetComponent<Rigidbody2D>());
        }

        private void Update()
        {
            //Vector2 xVelocity = _moveInput;
            if (_sensor.Umbrella)
            {
                //xVelocity = _moveInput + _sensor.Umbrella.Velocity;
                _mover.MoveOnTop(_moveInput, _sensor.Umbrella);
            }  
            else
            {
                _mover.Move(_moveInput);

            }
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
