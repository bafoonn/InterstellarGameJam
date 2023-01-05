using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jam
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class UmbrellaSensor : MonoBehaviour
    {
        private BoxCollider2D _collider;
        [field: SerializeField] public Umbrella Umbrella { get; private set; }

        private void Awake()
        {
            _collider = GetComponent<BoxCollider2D>();
            _collider.isTrigger = true;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.TryGetComponent(out Umbrella umbrella))
            {
                Umbrella = umbrella;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if(collision.TryGetComponent(out Umbrella umbrella) && Umbrella == umbrella)
            {
                Umbrella = null;
            }
        }
    }
}
