using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jam
{
    public class StageSelectState : GameStateBase
    {
        public override string SceneName => "StageSelect";

        public override StateType Type => StateType.StageSelect;

        public StageSelectState() : base()
        {
            AddTargetState(StateType.Stage);
            AddTargetState(StateType.MainMenu);
        }
    }
}
