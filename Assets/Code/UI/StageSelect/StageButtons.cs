using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Jam
{
    public class StageButtons : MonoBehaviour
    {
        [SerializeField] private int _firstStageIndex = 1;
        [SerializeField] private StageButton _buttonTemplate;

        private void Start()
        {
            int firstStageBuildIndex = SceneUtility.GetBuildIndexByScenePath($"Assets/Scenes/Stages/Stage{_firstStageIndex}.unity");

            bool validScene = true;
            int counter = 1;
            while(validScene)
            {
                string scenePath = SceneUtility.GetScenePathByBuildIndex(firstStageBuildIndex + counter);
                validScene = scenePath.Length > 0;
                counter++;
            }

            for (int i = 1; i < counter; i++)
            {
                StageButton newButton = Instantiate(_buttonTemplate, transform);
                newButton.Setup(i);
            }

            _buttonTemplate.gameObject.SetActive(false);
        }
    }
}
