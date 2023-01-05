using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Jam
{
    public abstract class LoaderBase : MonoBehaviour, ILoader
    {
        protected bool active;
        public bool Active => active;

        [SerializeField]
        protected float loadTime = 0.1f;
        public float LoadTime { get => loadTime; }

        public abstract LoaderType Type { get; }
        
        public abstract IEnumerator TransitionIn(float time);

        public abstract IEnumerator TransitionOut(float time);
    }

}
