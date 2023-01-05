using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jam
{
    public class StageEndState : GameStateBase
    {
        public override string SceneName => "StageEnd";

        public override StateType Type => StateType.StageEnd;

        public StageEndState() : base()
        {
            AddTargetState(StateType.MainMenu);
            AddTargetState(StateType.Stage);
            AddTargetState(StateType.StageSelect);
        }
    }
}
