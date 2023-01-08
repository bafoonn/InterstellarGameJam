using UnityEngine;

namespace Jam
{
    [System.Serializable]
    public struct StateClip
    {
        [field: SerializeField] public GameStateType State { get; private set; }
        [field: SerializeField] public AudioClip Clip { get; private set; }
    }
}
