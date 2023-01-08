using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jam
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class CharacterMover : MonoBehaviour
    {
        private Rigidbody2D _rigidbody;
        private SpriteRenderer _renderer;

        private float _currentSpeed;
        [Header("Movement")]
        [SerializeField] private float _speed = 5f;
        [SerializeField] private float _acceleration = 10f;
        [SerializeField] private float _deceleration = 10f;
        [SerializeField] private float _jumpHeight = 4;
        [SerializeField] private float _jumpSpeedMultiplier = 0.5f;
        private float _currentJumpMultiplier;
        
        [SerializeField]
        private Vector2 _movement;
        private Vector2 _velocity
        {
            get => _movement;
            set
            {
                _movement = new Vector2(Mathf.Clamp(value.x, _leftWallDistance, _rightWallDistance), value.y);
            }
        }

        [Header("WallCheck")]
        [SerializeField] private float _checkRange = 1f;
        [SerializeField] private Vector2 _checkOffset; 
        [SerializeField] private float _leftWallDistance;
        [SerializeField] private float _rightWallDistance;
        [SerializeField] private LayerMask _wallCheckLayer;

        private bool _isGrounded;
        [Header("GroundCheck")]
        [SerializeField] private Vector2 _groundCheck = new Vector2(0, -0.5f);
        [SerializeField] private float _checkRadius = 0.5f;
        [SerializeField] private LayerMask _groundCheckLayer;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _renderer = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            if (CheckIfGrounded() && _rigidbody.velocity.y < 0) _currentJumpMultiplier = 1;
            CheckWalls();
        }

        private bool CheckIfGrounded()
        {
            Vector2 checkPos = _rigidbody.position + _groundCheck;
            return _isGrounded = Physics2D.OverlapCircle(checkPos, _checkRadius, _groundCheckLayer);
        }

        private void CheckWalls()
        {
            bool isLeftWall = CheckDirection(Vector2.left);
            bool isRightWall = CheckDirection(Vector2.right);

            _leftWallDistance = isLeftWall ? 0 : -1;
            _rightWallDistance = isRightWall ? 0 : 1;
        }

        private bool CheckDirection(Vector2 direction)
        {
            if(direction.x > 0)
            {
                Vector2 min = _rigidbody.position + _checkOffset;
                Vector2 max = _rigidbody.position + new Vector2(_checkOffset.x, -_checkOffset.y);
                return CheckRays(min, max, direction);
            }
            else
            {
                Vector2 min = _rigidbody.position + new Vector2(-_checkOffset.x, _checkOffset.y);
                Vector2 max = _rigidbody.position + new Vector2(-_checkOffset.x, -_checkOffset.y);
                return CheckRays(min, max, direction);
            }
        }

        private bool CheckRays(Vector2 min, Vector2 max, Vector2 direction)
        {
            float t = 0;
            int rayCount = 3;
            for (int i = 0; i < rayCount; i++)
            {
                t = (float)i / (rayCount - 1);
                Vector2 pos = Vector2.Lerp(min, max, t);
                Debug.DrawRay(pos, direction * _checkRange);
                if (Physics2D.Raycast(pos, direction, _checkRange, _wallCheckLayer)) return true;
            }
            return false;
        }

        /// <summary>
        /// Returns the current speed based on movement input.<br></br>
        /// Changes private vector field to enable acceleration/deceleration.
        /// </summary>
        /// <param name="moveInput">Input which determines if the speed is accelerating or decelerating.</param>
        /// <returns></returns>
        private float GetSpeed(Vector2 moveInput)
        {
            if (moveInput.Equals(Vector2.zero))
            {
                return Mathf.MoveTowards(_currentSpeed, 0, _deceleration * Time.deltaTime);
            }
            else
            {
                _velocity = transform.right * moveInput.x * _currentSpeed;
                return Mathf.MoveTowards(_currentSpeed, _speed, _acceleration * Time.deltaTime);
            }
        }

        /// <summary>
        /// Flips the sprite on the x-axis based on velocity;
        /// </summary>
        /// <param name="xVelocity">Determines if the sprite is flipped. Positive = flipped</param>
        private void FlipSprite(float xVelocity)
        {
            if(xVelocity > 0)
            {
                _renderer.flipX = true;
            }
            else if(xVelocity < 0)
            {
                _renderer.flipX = false;
            }
        }

        /// <summary>
        /// Moves the character on the x-axis.
        /// </summary>
        /// <param name="moveInput">Determines the movement direction.</param>
        public void Move(Vector2 moveInput)
        {
            _currentSpeed = GetSpeed(moveInput);

            FlipSprite(moveInput.x);
            
            _rigidbody.velocity = new Vector2(_velocity.x * _currentSpeed * _currentJumpMultiplier, _rigidbody.velocity.y);
        }

        /// <summary>
        /// Moves the character based on the umbrellas movement and movement input.
        /// </summary>
        /// <param name="moveInput">Determines the movement direction.<br>Is added to umbrellas velocity.</br></param>
        /// <param name="rigidbody">Rigidbodys velocity is added to CharacterMovers velocity.</param>
        public void MoveOnTop(Vector2 moveInput, Rigidbody2D rigidbody)
        {
            _currentSpeed = GetSpeed(moveInput);

            float xVelocity = rigidbody.velocity.x + _velocity.x * _currentSpeed * _currentJumpMultiplier;
            FlipSprite(moveInput.x);

            _rigidbody.velocity = new Vector2(xVelocity, _rigidbody.velocity.y);
        }

        /// <summary>
        /// Makes the character jump a certain height.
        /// </summary>
        public void Jump()
        {
            if(_isGrounded)
            {
                float jumpVelocity = Mathf.Sqrt(_jumpHeight * -2f * Physics2D.gravity.y);
                _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, jumpVelocity);
                _currentJumpMultiplier = 1 + _jumpSpeedMultiplier * (_currentSpeed / _speed);
                AudioManager.Instance.PlaySound("Jump");
            }
        }

        public void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Vector2 pos = (Vector2)transform.position + _groundCheck;
            Gizmos.DrawWireSphere(pos, _checkRadius);
        }
    }
}
