using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Jam
{
    public class StageManager : Singleton<StageManager>
    {
        [SerializeField] private Player _bluePlayerPrefab;
        [SerializeField] private Player _redPlayerPrefab;
        private PlayerSpawnPoint[] _spawnPoints;
        private bool _isBlueOnExit = false;
        private bool _isRedOnExit = false;

        [field: SerializeField]
        public bool IsPaused { get; private set; } = false;

        private void Start()
        {
            _spawnPoints = FindObjectsOfType<PlayerSpawnPoint>();

            Debug.Assert(_spawnPoints.Any(spawn => spawn.Color == PlayerColor.Blue), $"{name} cannot find BlueSpawnPoint.");
            Debug.Assert(_spawnPoints.Any(spawn => spawn.Color == PlayerColor.Red), $"{name} cannot find RedSpawnPoint.");

            PlayerSpawnPoint blueSpawn = _spawnPoints.Single(spawn => spawn.Color == PlayerColor.Blue);
            Player blue = Instantiate(_bluePlayerPrefab, blueSpawn.transform.position, blueSpawn.transform.rotation);
            blue.Setup(this);

            PlayerSpawnPoint redSpawn = _spawnPoints.Single(spawn => spawn.Color == PlayerColor.Red);
            Player red = Instantiate(_redPlayerPrefab, redSpawn.transform.position, redSpawn.transform.rotation);
            red.Setup(this);
        }

        private void OnEnable()
        {
            GameStateManager.StateChanged += OnGameStateChanged;
            Portal.Entered += OnPortalEntered;
            Portal.Exited += OnPortalExited;
        }

        private void OnDisable()
        {
            GameStateManager.StateChanged -= OnGameStateChanged;
            Portal.Entered -= OnPortalEntered;
            Portal.Exited -= OnPortalExited;
        }

        private void OnPortalEntered(PlayerColor color)
        {
            switch (color)
            {
                case PlayerColor.Red:
                    _isRedOnExit = true;
                    break;
                case PlayerColor.Blue:
                    _isBlueOnExit = true;
                    break;
            }

            if (_isRedOnExit && _isBlueOnExit) GameStateManager.Instance.Go(GameStateType.StageEnd);
        }

        private void OnPortalExited(PlayerColor color)
        {
            switch (color)
            {
                case PlayerColor.Red:
                    _isRedOnExit = false;
                    break;
                case PlayerColor.Blue:
                    _isBlueOnExit = false;
                    break;
            }
        }

        private void OnGameStateChanged(GameStateBase state)
        {
            switch(state.Type)
            {
                case GameStateType.Options:
                    IsPaused = true;
                    break;

                case GameStateType.StageEnd:
                    IsPaused = true;
                    break;

                default:
                    IsPaused = false;
                    break;
            }
        }

        public void OnPause(InputAction.CallbackContext callback)
        {
            if (callback.started && IsPaused == false)
            {
                GameStateManager.Instance.Go(GameStateType.Options, false);
            }
            else if (callback.started && IsPaused == true)
            {
                GameStateManager.Instance.GoBack();
            }
        }
    }
}