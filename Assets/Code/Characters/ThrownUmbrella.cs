using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jam
{
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class ThrownUmbrella : MonoBehaviour
    {
        private BoxCollider2D _collider;
        private Rigidbody2D _rigidbody;
        private SpriteRenderer _renderer;
        private LayerMask _checkLayer;
        [SerializeField] private float _checkOffset;
        [SerializeField] private float _speed = 5;

        private Umbrella _parent;

        private void Awake()
        {
            _collider = GetComponent<BoxCollider2D>();
            _rigidbody = GetComponent<Rigidbody2D>();
            _renderer = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            if (_parent.IsThrown) CheckForWalls();
        }

        public void Throw(Vector2 position, Vector2 direction)
        {
            Reset(position);

            _rigidbody.velocity = direction * _speed;
        }

        private void CheckForWalls()
        {
            Vector2 pos = (Vector2)transform.position + _parent.ThrowDirection * _checkOffset;
            bool isHit = Physics2D.OverlapCircle(pos, 0.25f, _checkLayer);
            if (isHit) Stop();
        }

        private void Stop()
        {
            _collider.enabled = true;
            _rigidbody.velocity = Vector2.zero;

        }

        public void Recall()
        {
            Disable();
        }

        public void Setup(Umbrella parent, Color color, LayerMask checkLayer)
        {
            _parent = parent;
            _checkLayer = checkLayer;
            _renderer.color = color;
            gameObject.layer = parent.gameObject.layer;
            Disable();
        }

        private void Reset(Vector2 position)
        {
            gameObject.SetActive(true);
            transform.position = position;
            _renderer.enabled = true;
        }

        private void Disable()
        {
            _collider.enabled = false;
            _renderer.enabled = false;
            gameObject.SetActive(false);
        }
    }
}
