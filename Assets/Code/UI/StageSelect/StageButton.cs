using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Jam
{
    [RequireComponent(typeof(Button))]
    public class StageButton : MonoBehaviour
    {
        private Button _button;
        private TextMeshProUGUI _tmp;
        private int _stageIndex;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _tmp = GetComponentInChildren<TextMeshProUGUI>();
        }

        private void OnButtonClick()
        {
            GameStateManager.Instance.Go(GameStateType.Stage, false, _stageIndex);
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnButtonClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnButtonClick);
        }

        public void Setup(int stageIndex)
        {
            _stageIndex = stageIndex;
            _tmp.text = _stageIndex.ToString();
            gameObject.SetActive(true);
        }
    }
}
