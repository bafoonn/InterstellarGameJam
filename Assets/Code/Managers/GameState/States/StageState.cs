using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jam
{
    public class StageState : GameStateBase
    {
        public override string SceneName => $"Stage{_stageIndex}";

        public override StateType Type => StateType.Stage;

        public StageState() : base()
        {
            AddTargetState(StateType.MainMenu);
            AddTargetState(StateType.Options);
            AddTargetState(StateType.StageEnd);
        }
    }
}
