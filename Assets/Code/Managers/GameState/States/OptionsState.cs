using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jam
{
    public class OptionsState : GameStateBase
    {
        public override string SceneName => "Options";

        public override StateType Type => StateType.Options;

        public OptionsState() : base()
        {
            AddTargetState(StateType.MainMenu);
            AddTargetState(StateType.Stage);
        }
    }
}
