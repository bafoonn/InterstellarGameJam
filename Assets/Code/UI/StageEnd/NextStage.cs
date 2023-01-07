using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Jam
{
    [RequireComponent(typeof(Button))]
    public class NextStage : MonoBehaviour
    {
        private int _nextStageIndex;
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();

            _button.interactable = NextStageExists();
        }

        private bool NextStageExists()
        {
            _nextStageIndex = GameStateManager.Instance.StageIndex + 1;
            Scene nextStage = SceneManager.GetSceneByName($"Stage{_nextStageIndex}");
            return nextStage.IsValid();
        }
    }
}
