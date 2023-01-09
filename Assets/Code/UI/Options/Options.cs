using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Jam
{
    public class Options : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _pauseObjects = new();
        [SerializeField] private List<GameObject> _optionsObjects = new();

        private void Start()
        {
            switch(GameStateManager.Instance.PreviousState.Type)
            {
                case GameStateType.Stage:
                    _pauseObjects.ForEach(obj => obj.SetActive(true));
                    _optionsObjects.ForEach(obj => obj.SetActive(false));
                    break;
                default:
                    _pauseObjects.ForEach(obj => obj.SetActive(false));
                    _optionsObjects.ForEach(obj => obj.SetActive(true));
                    break;
            }
        }

        public void Back()
        {
            GameStateManager.Instance.GoBack();
        }

        public void MainMenu()
        {
            GameStateManager.Instance.Go(GameStateType.MainMenu);
        }

        public void Credits()
        {
            GameStateManager.Instance.Go(GameStateType.Credits);
        }

        public void StageSelect()
        {
            GameStateManager.Instance.Go(GameStateType.StageSelect);
        }
    }
}
