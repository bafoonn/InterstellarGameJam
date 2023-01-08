using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Jam
{
    [RequireComponent(typeof(CharacterMover))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(PlayerInput))]
    public class Player : MonoBehaviour, IPlayerColor
    {
        // Private Implementation
        private CharacterMover _mover;
        private PlayerInput _playerInput;
        private Umbrella _umbrella;
        private UmbrellaSensor _sensor;
        private PlayerAnimations _animations;

        private StageManager _manager;

        [SerializeField] private PlayerColor _playerColor;

        public Vector2 MoveInput { get; private set; }
        public bool HoldUpInput { get; private set; }

        public PlayerColor Color => _playerColor;

        private void Awake()
        {
            _mover = GetComponent<CharacterMover>();
            _umbrella = GetComponentInChildren<Umbrella>();
            _sensor = GetComponentInChildren<UmbrellaSensor>();
            _animations = GetComponentInChildren<PlayerAnimations>();
            _playerInput = GetComponent<PlayerInput>();

            Debug.Assert(_sensor != null, $"{name} cannot find MovementSensor component in children.");
            Debug.Assert(_umbrella != null, $"{name} cannot find Umbrella component in children.");
            Debug.Assert(_animations != null, $"{name} cannot find PlayerAnimations component in children.");

            _umbrella.Setup(this, GetComponent<Rigidbody2D>());
        }

        private void Update()
        {
            if(_manager) _playerInput.enabled = !_manager.IsPaused;

            if (_sensor.Umbrella)
            {
                _mover.MoveOnTop(MoveInput, _sensor.Umbrella.Rigidbody);
            }  
            else
            {
                _mover.Move(MoveInput);
            }

            _animations.UpdateAnimations(MoveInput, _mover.IsGrounded, HoldUpInput, _umbrella.IsThrown);
        }

        public void OnMove(InputAction.CallbackContext callback)
        {
            MoveInput = callback.ReadValue<Vector2>();
        }

        public void OnHoldUp(InputAction.CallbackContext callback)
        {
            if(callback.started && !_umbrella.IsThrown)
            {
                AudioManager.Instance.PlaySound("UmbrellaUp");
            }
            HoldUpInput = callback.performed;
        }

        public void OnJump(InputAction.CallbackContext callback)
        {
            if(callback.started)
            {
                _mover.Jump();
                _animations.Jump();
            }
        }

        public void OnThrow(InputAction.CallbackContext callback)
        {
            if (callback.started)
            {
                _umbrella.Throw();
                if(_umbrella.IsThrown)
                {
                    _animations.Throw();
                    AudioManager.Instance.PlaySound("Throw");
                }
            }
        }

        public void Setup(StageManager manager)
        {
            _manager = manager;
        }
    }
}
