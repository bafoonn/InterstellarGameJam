using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jam
{
    public class StageSelectState : GameStateBase
    {
        public override string SceneName => "StageSelect";

        public override GameStateType Type => GameStateType.StageSelect;

        public StageSelectState() : base()
        {
            AddTargetState(GameStateType.Stage);
            AddTargetState(GameStateType.MainMenu);
        }
    }
}
