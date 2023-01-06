using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jam
{
    public class MainMenuState : GameStateBase
    {
        public override string SceneName => "MainMenu";

        public override GameStateType Type => GameStateType.MainMenu;

        public MainMenuState() : base()
        {
            AddTargetState(GameStateType.StageSelect);
            AddTargetState(GameStateType.Options);
            AddTargetState(GameStateType.Credits);
        }
    }
}
