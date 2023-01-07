using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Jam
{
    public class MainMenu : MonoBehaviour
    {
        public void StageSelect()
        {
            GameStateManager.Instance.Go(GameStateType.StageSelect);
        }

        public void Options()
        {
            GameStateManager.Instance.Go(GameStateType.Options);
        }

        public void Exit()
        {
#if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
#else
            Application.Quit();
#endif
        }
    }
}
