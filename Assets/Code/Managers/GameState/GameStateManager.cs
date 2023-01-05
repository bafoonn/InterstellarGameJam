using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Jam
{
	/// <summary>
	/// Controls the game's global state. There should be exactly one instance of this object
	/// at any given time.
	/// </summary>
	public class GameStateManager : PersistentSingleton<GameStateManager>
	{
	
		#region Fields
		private List<GameStateBase> _states = new List<GameStateBase>();

		public static event Action<GameStateBase> StateChanged;

		private ILoader _currentLoader;
		private ILoader[] _loaders;
        #endregion

        #region Properties
        public GameStateBase CurrentState
		{
			get;
			private set;
		}

		public GameStateBase PreviousState
		{
			get;
			private set;
		}
        #endregion

        #region Private implementation
        protected override void Awake()
        {
            base.Awake();
			_loaders = GetComponentsInChildren<ILoader>();
        }

        protected override void Init()
        {
			// Create state objects
			MainMenuState mainMenu = new MainMenuState();
			OptionsState optionsState = new OptionsState();
			StageSelectState stageSelect = new StageSelectState();
			StageState stage = new StageState();
			StageEndState stageEnd = new StageEndState();
			CreditsState credits = new CreditsState();

			_states.Add(mainMenu);
			_states.Add(stageSelect);
			_states.Add(optionsState);
			_states.Add(stage);
			_states.Add(stageEnd);
			_states.Add(credits);

			string activeSceneName = SceneManager.GetActiveScene().name.ToLower();
			foreach(GameStateBase state in _states)
			{
				if (state.SceneName.ToLower() == activeSceneName)
				{
					ActivateFirstScene(state);
					break; // Early exit from the loop
				}
			}

			if (CurrentState == null)
			{
				ActivateFirstScene(mainMenu);
			}
		}

		private void ActivateFirstScene(GameStateBase first)
		{
			CurrentState = first;
			CurrentState.Activate();
		}

		private GameStateBase GetState(StateType type)
		{
			foreach (GameStateBase state in _states)
			{
				if (state.Type == type)
				{
					return state;
				}
			}

			return null;
		}

		private ILoader GetLoader(GameStateBase state)
        {
			return _loaders.SingleOrDefault(loader => loader.Type == state.Loader);
		}


		private IEnumerator TransitionTo(GameStateBase next, bool forceLoad = false)
        {
			StartCoroutine(_currentLoader.TransitionIn(_currentLoader.LoadTime));
			yield return new WaitUntil(() => _currentLoader.Active == true);
			PreviousState = CurrentState;
			PreviousState.Deactivate();

			CurrentState = next;
			CurrentState.Activate(forceLoad);
			yield return new WaitForEndOfFrame();
			StartCoroutine(_currentLoader.TransitionOut(_currentLoader.LoadTime));
			StateChanged?.Invoke(CurrentState);
        }
		#endregion

		#region Public API
		/// <summary>
		/// Transitions from current state to the target state.
		/// </summary>
		/// <param name="targetStateType">The type of the target state.</param>
		/// <returns>True, if transition is legal and can be done. False otherwise.</returns>
		public bool Go(StateType targetStateType, bool forceLoad, int stageIndex = 0)
		{
			// Check the legality of the transition
			if (!CurrentState.IsValidTarget(targetStateType))
			{
				Debug.Log($"{targetStateType} is not valid target for {CurrentState.Type}");
				return false;
			}

			// Find the state that matches the targetStateType
			GameStateBase nextState = GetState(targetStateType);
			if (nextState == null)
			{
				Debug.Log($"No state exists that represents the {targetStateType}");
				return false;
			}

			// Select loader based on next state
			_currentLoader = GetLoader(nextState);

			// Transition from current state to the target state
			if(_currentLoader == null)
			{
				PreviousState = CurrentState;
				PreviousState.Deactivate();

				CurrentState = nextState;
				CurrentState.Activate(forceLoad);
				StateChanged?.Invoke(CurrentState);
			}
			else StartCoroutine(TransitionTo(nextState, forceLoad));

			if (nextState.Type == StateType.Stage)
            {
				Cursor.visible = false;
            }
			else
            {
				Cursor.visible = true;
            }

			return true;
		}

		/// <summary>
		/// Transitions back to the previous state.
		/// </summary>
		/// <returns>True, if the transition succeeds. False otherwise.</returns>
		public bool GoBack()
		{
			return Go(PreviousState.Type, false);
		}
		#endregion
	}
}
