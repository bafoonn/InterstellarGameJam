using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jam
{
    public class Umbrella : MonoBehaviour
    {
        public Rigidbody2D Rigidbody { get; private set; }
        public Vector2 Velocity => Rigidbody.velocity;

        public void Setup(Rigidbody2D rigidbody)
        {
            Rigidbody = rigidbody;
        }
    }
}
