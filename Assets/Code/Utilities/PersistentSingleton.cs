using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jam
{
    /// <summary>
    /// Singleton pattern that persists through scenes.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PersistentSingleton<T> : Singleton<T> where T : MonoBehaviour
    {
        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(gameObject);
        }
    }
}
