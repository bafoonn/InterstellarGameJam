using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jam
{
    public class StageEnd : MonoBehaviour
    {
        public void NextStage()
        {
            int nextStageIndex = GameStateManager.Instance.StageIndex + 1;
            GameStateManager.Instance.Go(GameStateType.Stage, false, nextStageIndex);
        }

        public void StageSelect()
        {
            GameStateManager.Instance.Go(GameStateType.StageSelect);
        }

        public void MainMenu()
        {
            GameStateManager.Instance.Go(GameStateType.MainMenu);
        }
    }
}
