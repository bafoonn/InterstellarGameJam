using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jam
{
    public class CreditsState : GameStateBase
    {
        public override string SceneName => "Credits";

        public override GameStateType Type => GameStateType.Credits;

        public CreditsState() : base()
        {
            AddTargetState(GameStateType.MainMenu);
            AddTargetState(GameStateType.Options);
        }
    }
}
