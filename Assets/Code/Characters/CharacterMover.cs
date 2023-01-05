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

        public void Move(Vector2 moveInput)
        {
            
            if(moveInput.Equals(Vector2.zero))
            {
                _currentSpeed = Mathf.MoveTowards(_currentSpeed, 0, _deceleration * Time.deltaTime);
            }
            else
            {
                _currentSpeed = Mathf.MoveTowards(_currentSpeed, _speed, _acceleration * Time.deltaTime);
                _movement = transform.right * moveInput.x;
            }


            //_rigidbody.MovePosition(_rigidbody.position + movement * _currentSpeed * Time.deltaTime);
            _rigidbody.velocity = new Vector2(_movement.x * _currentSpeed, _rigidbody.velocity.y);
        }

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
