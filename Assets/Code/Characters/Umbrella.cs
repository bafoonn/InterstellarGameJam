using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Jam
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class Umbrella : MonoBehaviour
    {
        private Player _player;
        private BoxCollider2D _collider;
        public bool IsThrown { get; private set; }
        public Vector2 ThrowDirection { get; private set; } = Vector2.left;

        [SerializeField] private ThrownUmbrella _thrownUmbrellaPrefab;
        [SerializeField] private LayerMask _stopLayers;
        private ThrownUmbrella _thrownUmbrella;

        public Rigidbody2D Rigidbody { get; private set; }
        public Vector2 Velocity => Rigidbody.velocity;

        private void Awake()
        {
            _collider = GetComponent<BoxCollider2D>();

            Debug.Assert(_thrownUmbrellaPrefab != null, $"{name} has not been set in the inspector.");

            _thrownUmbrella = Instantiate(_thrownUmbrellaPrefab);
            _thrownUmbrella.Setup(this, _stopLayers);
        }

        private void Update()
        {
            if (!_player.MoveInput.Equals(Vector2.zero)) ThrowDirection = _player.MoveInput;

            if(_player.HoldUpInput && !IsThrown)
            {
                HoldUp();
            }
            else
            {
                _collider.enabled = false;
            }
        }

        private void HoldUp()
        {
            _collider.enabled = true;
        }

        public void Throw()
        {
            if(IsThrown)
            {
                Recall();
                return;
            }

            
            IsThrown = !IsThrown;

            _thrownUmbrella.Throw(Rigidbody.position, ThrowDirection);

            Debug.Assert(IsThrown == true);
        }

        private void Recall()
        {
            _thrownUmbrella.Recall();

            IsThrown = !IsThrown;

        }

        public void Setup(Player player, Rigidbody2D rigidbody)
        {
            _player = player;
            Rigidbody = rigidbody;
        }
    }
}
