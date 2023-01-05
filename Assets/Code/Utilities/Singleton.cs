using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jam
{
    /// <summary>
    /// Singleton pattern.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T instance;
        public static T Instance
        {
            get
            {
                if(instance == null)
                {
                    T prefab = Resources.Load<T>($"Singletons/{typeof(T).Name}");

                    if(prefab)
                    {
                        instance = Instantiate(prefab);
                    }
                    else
                    {
                        Debug.LogWarning(typeof(T).Name + " was not found in resources." + typeof(T).Name + " was not loaded!");
                    }
                }

                return instance;
            }
        }

        protected virtual void Awake()
        {
            if (instance == null)
            {
                instance = this as T;
            }
            else if (instance != this)
            {
                Destroy(this.gameObject);
                return;
            }

            Init();
        }

        protected virtual void Init()
        {

        }
    }
}
