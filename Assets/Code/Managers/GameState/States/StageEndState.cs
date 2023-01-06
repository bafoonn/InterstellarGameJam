using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jam
{
    public class StageEndState : GameStateBase
    {
        public override string SceneName => "StageEnd";

        public override GameStateType Type => GameStateType.StageEnd;

        public StageEndState() : base()
        {
            AddTargetState(GameStateType.MainMenu);
            AddTargetState(GameStateType.Stage);
            AddTargetState(GameStateType.StageSelect);
        }
    }
}
