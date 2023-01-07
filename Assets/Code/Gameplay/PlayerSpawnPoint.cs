using UnityEngine;

namespace Jam
{
    public abstract class PlayerSpawnPoint : MonoBehaviour, IPlayerColor
    {
        protected virtual PlayerColor _color => PlayerColor.None;

        public PlayerColor Color => _color;
    }
}
