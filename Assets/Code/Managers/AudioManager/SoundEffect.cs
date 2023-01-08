using UnityEngine;

namespace Jam
{
    [System.Serializable]
    public struct SoundEffect
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public AudioClip Clip { get; private set; }
    }

}
