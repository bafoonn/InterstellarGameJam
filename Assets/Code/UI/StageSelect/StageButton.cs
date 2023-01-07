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
        [SerializeField] private int _stageIndex;
        public int Index => _stageIndex;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _tmp = GetComponentInChildren<TextMeshProUGUI>();
            _tmp.text = Index.ToString();
        }

        private void OnButtonClick()
        {
            GameStateManager.Instance.Go(GameStateType.Stage, false, Index);
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnButtonClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnButtonClick);
        }
    }
}
