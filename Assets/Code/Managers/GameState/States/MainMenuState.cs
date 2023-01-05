using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jam
{
    public class MainMenuState : GameStateBase
    {
        public override string SceneName => "MainMenu";

        public override StateType Type => StateType.MainMenu;

        public MainMenuState() : base()
        {
            AddTargetState(StateType.StageSelect);
            AddTargetState(StateType.Options);
            AddTargetState(StateType.Credits);
        }
    }
}
