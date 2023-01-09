using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Jam
{
    public class ButtonHandler : MonoBehaviour
    {
        [SerializeField]
        private Button[] buttons;
        [SerializeField]
        private GameStateType type;
        private EventSystem eventSystem;

        private void Awake()
        {
            buttons = GetComponentsInChildren<Button>();
            eventSystem = EventSystem.current;
        }

        private void Start()
        {
            SetButton();
        }

        private void Update()
        {
            if (!eventSystem.currentSelectedGameObject)
            {
                SetButton();
            }
            else if (eventSystem.currentSelectedGameObject.activeInHierarchy == false)
            {
                SetButton();
            }
        }

        private void SetButton()
        {
            var button = buttons.First(b => b.gameObject.activeInHierarchy);
            eventSystem.SetSelectedGameObject(button.gameObject);
        }

        private void OnEnable()
        {
            GameStateManager.StateChanged += OnStateChanged;
        }

        private void OnDisable()
        {
            GameStateManager.StateChanged -= OnStateChanged;
        }

        private void OnStateChanged(GameStateBase state)
        {
            if (state.Type == type)
            {
                foreach (var button in buttons)
                {
                    button.interactable = true;
                    SetButton();
                }
            }
            else
            {
                foreach (var button in buttons)
                {
                    button.interactable = false;
                }
            }
        }
    }
}
