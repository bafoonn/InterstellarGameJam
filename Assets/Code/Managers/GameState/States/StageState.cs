using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jam
{
    public class StageState : GameStateBase
    {
        public override string SceneName => $"Stage{_stageIndex}";

        public override GameStateType Type => GameStateType.Stage;

        public StageState() : base()
        {
            AddTargetState(GameStateType.MainMenu);
            AddTargetState(GameStateType.Options);
            AddTargetState(GameStateType.Stage);
            AddTargetState(GameStateType.StageEnd);
        }
    }
}
