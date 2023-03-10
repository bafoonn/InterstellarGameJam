using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jam
{
    public class StageEndState : GameStateBase
    {
        public override string SceneName => "StageEnd";

        public override GameStateType Type => GameStateType.StageEnd;

        public override bool IsAdditive => true;

        public override LoaderType Loader => LoaderType.None;

        public StageEndState() : base()
        {
            AddTargetState(GameStateType.MainMenu);
            AddTargetState(GameStateType.Stage);
            AddTargetState(GameStateType.StageSelect);
        }
    }
}
