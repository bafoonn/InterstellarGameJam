using System;
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

        public Vector2 MoveInput { get; private set; }
        public bool HoldUpInput { get; private set; }

        private void Awake()
        {
            _mover = GetComponent<CharacterMover>();
            _umbrella = GetComponentInChildren<Umbrella>();
            _sensor = GetComponentInChildren<UmbrellaSensor>();

            Debug.Assert(_sensor != null, $"{name} cannot find MovementSensor component in children.");
            Debug.Assert(_umbrella != null, $"{name} cannot find Umbrella component in children.");

            _umbrella.Setup(this, GetComponent<Rigidbody2D>());
        }

        private void Update()
        {
            //Vector2 xVelocity = _moveInput;
            if (_sensor.Umbrella)
            {
                //xVelocity = _moveInput + _sensor.Umbrella.Velocity;
                _mover.MoveOnTop(MoveInput, _sensor.Umbrella.Rigidbody);
            }  
            else
            {
                _mover.Move(MoveInput);

            }
        }

        public void OnMove(InputAction.CallbackContext callback)
        {
            MoveInput = callback.ReadValue<Vector2>();
        }

        public void OnHoldUp(InputAction.CallbackContext callback)
        {
            HoldUpInput = callback.performed;
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
            if (callback.started)
            {
                //Throw?.Invoke();
                _umbrella.Throw();
                callback.action.WasPerformedThisFrame();
            }

        }
    }
}
