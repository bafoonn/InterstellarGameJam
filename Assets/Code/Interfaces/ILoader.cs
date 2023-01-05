using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jam
{
    public interface ILoader
    {
        public bool Active { get; }
        public float LoadTime { get; }
        public LoaderType Type { get; }
        public IEnumerator TransitionIn(float time);
        public IEnumerator TransitionOut(float time);
    }
}
