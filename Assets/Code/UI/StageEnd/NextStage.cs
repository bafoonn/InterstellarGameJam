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
            Scene scene = SceneManager.GetSceneByName($"Stage{GameStateManager.Instance.StageIndex}");
            string path = SceneUtility.GetScenePathByBuildIndex(scene.buildIndex + 1);
            return path.Length > 0;
        }
    }
}
