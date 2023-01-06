using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jam
{
    public class OptionsState : GameStateBase
    {
        public override string SceneName => "Options";

        public override GameStateType Type => GameStateType.Options;

        public OptionsState() : base()
        {
            AddTargetState(GameStateType.MainMenu);
            AddTargetState(GameStateType.Stage);
        }
    }
}
