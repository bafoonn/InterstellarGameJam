using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jam
{
    public class CreditsState : GameStateBase
    {
        public override string SceneName => "Credits";

        public override StateType Type => StateType.Credits;

        public CreditsState() : base()
        {
            AddTargetState(StateType.MainMenu);
        }
    }
}
