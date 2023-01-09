using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jam
{
    public class Credits : MonoBehaviour
    {
        public void MainMenu()
        {
            GameStateManager.Instance.Go(GameStateType.MainMenu);
        }

        public void Back()
        {
            GameStateManager.Instance.GoBack();
        }
    }
}
