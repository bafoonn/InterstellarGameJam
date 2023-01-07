using UnityEngine;

namespace Jam
{
    public class StageSelect : MonoBehaviour
    {
        public void MainMenu()
        {
            GameStateManager.Instance.Go(GameStateType.MainMenu);
        }
    }
}
