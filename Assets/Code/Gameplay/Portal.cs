using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jam
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class Portal : MonoBehaviour
    {
        private BoxCollider2D _collider;
        [SerializeField] private PlayerColor _color;

        public static event Action<PlayerColor> Entered;
        public static event Action<PlayerColor> Exited;

        private void Awake()
        {
            _collider = GetComponent<BoxCollider2D>();

            Debug.Assert(_collider != null, $"{name} cannot find BoxCollider2D component on the GameObject.");

            _collider.isTrigger = true;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.TryGetComponent(out IPlayerColor player))
            {
                if (player.Color == _color) Entered?.Invoke(player.Color);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if(collision.TryGetComponent(out IPlayerColor player))
            {
                if (player.Color == _color) Exited?.Invoke(player.Color);
            }
        }
    }
}
