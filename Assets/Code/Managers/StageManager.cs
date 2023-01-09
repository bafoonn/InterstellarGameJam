using System;
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
        private Player _bluePlayer;
        private Player _redPlayer;
        private PlayerSpawnPoint[] _spawnPoints;
        private bool _isBlueOnPortal = false;
        private bool _isRedOnPortal = false;

        public bool IsPaused { get; private set; } = false;
        public event Action StageEnd;

        private void Start()
        {
            _spawnPoints = GetComponentsInChildren<PlayerSpawnPoint>();

            Debug.Assert(_spawnPoints.Any(spawn => spawn.Color == PlayerColor.Blue), $"{name} cannot find BlueSpawnPoint.");
            Debug.Assert(_spawnPoints.Any(spawn => spawn.Color == PlayerColor.Red), $"{name} cannot find RedSpawnPoint.");

            PlayerSpawnPoint blueSpawn = _spawnPoints.Single(spawn => spawn.Color == PlayerColor.Blue);
            PlayerSpawnPoint redSpawn = _spawnPoints.Single(spawn => spawn.Color == PlayerColor.Red);

            _bluePlayer = Instantiate(_bluePlayerPrefab, blueSpawn.transform.position, blueSpawn.transform.rotation);
            _redPlayer = Instantiate(_redPlayerPrefab, redSpawn.transform.position, redSpawn.transform.rotation);
            AudioManager.Instance.PlaySound("ExitPortal");
            _bluePlayer.Setup(this);
            _redPlayer.Setup(this);
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

        private void Update()
        {
            if(_redPlayer.transform.position.y < -20)
            {
                Vector2 spawnPoint = _spawnPoints.Single(spawn => spawn.Color == PlayerColor.Red).transform.position;
                _redPlayer.transform.position = spawnPoint;
            }
            else if(_bluePlayer.transform.position.y < -20)
            {
                Vector2 spawnPoint = _spawnPoints.Single(spawn => spawn.Color == PlayerColor.Blue).transform.position;
                _bluePlayer.transform.position = spawnPoint;
            }
        }

        private void OnPortalEntered(PlayerColor color)
        {
            switch (color)
            {
                case PlayerColor.Red:
                    _isRedOnPortal = true;
                    break;
                case PlayerColor.Blue:
                    _isBlueOnPortal = true;
                    break;
            }

            if (_isRedOnPortal && _isBlueOnPortal)
            {
                AudioManager.Instance.PlaySound("EnterPortal");
                GameStateManager.Instance.Go(GameStateType.StageEnd);
                StageEnd?.Invoke();
            }
        }

        private void OnPortalExited(PlayerColor color)
        {
            switch (color)
            {
                case PlayerColor.Red:
                    _isRedOnPortal = false;
                    break;
                case PlayerColor.Blue:
                    _isBlueOnPortal = false;
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

        public void OnRestart(InputAction.CallbackContext callback)
        {
            if (callback.started) GameStateManager.Instance.RestartStage();
        }
    }
}