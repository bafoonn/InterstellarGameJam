using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jam
{
    [RequireComponent(typeof(Rigidbody2D)), RequireComponent(typeof(SpriteRenderer))]
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
        private Vector2 _movement = Vector2.zero;

        private bool _isGrounded;
        [Header("GroundCheck")]
        [SerializeField] private Vector2 _groundCheck = new Vector2(0, -0.5f);
        [SerializeField] private float _checkRadius = 0.5f;
        [SerializeField] private LayerMask _checkLayer;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _renderer = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            Vector2 checkPos = _rigidbody.position + _groundCheck;
            _isGrounded = Physics2D.OverlapCircle(checkPos, _checkRadius, _checkLayer);
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
                _movement = transform.right * moveInput.x;
                return Mathf.MoveTowards(_currentSpeed, _speed, _acceleration * Time.deltaTime);
            }
        }

        /// <summary>
        /// Flips the sprite on the x-axis based on velocity;
        /// </summary>
        /// <param name="xVelocity">Determines if the sprite is flipped. Positive = flipped</param>
        private void FlipSprite(float xVelocity)
        {
            _renderer.flipX = xVelocity > 0;    
        }

        /// <summary>
        /// Moves the character on the x-axis.
        /// </summary>
        /// <param name="moveInput">Determines the movement direction.</param>
        public void Move(Vector2 moveInput)
        {
            _currentSpeed = GetSpeed(moveInput);

            FlipSprite(_movement.x);
            
            _rigidbody.velocity = new Vector2(_movement.x * _currentSpeed, _rigidbody.velocity.y);
        }

        /// <summary>
        /// Moves the character based on the umbrellas movement and movement input.
        /// </summary>
        /// <param name="moveInput">Determines the movement direction.<br>Is added to umbrellas velocity.</br></param>
        /// <param name="umbrella">Umbrellas velocity is added to CharacterMovers velocity.</param>
        public void MoveOnTop(Vector2 moveInput, Umbrella umbrella)
        {
            _currentSpeed = GetSpeed(moveInput);

            float xVelocity = umbrella.Velocity.x + _movement.x * _currentSpeed;

            FlipSprite(_movement.x);

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
