using System;
using GlassyCode.CannonDefense.Core.Time;
using GlassyCode.CannonDefense.Game.Enemies.Logic;
using GlassyCode.CannonDefense.Game.Player.Logic;
using Zenject;

namespace GlassyCode.CannonDefense.Game.Battlefield.Logic
{
    public class BattlefieldManager : IBattlefieldManager, IDisposable
    {
        private ITimeController _timeController;
        private IPlayerManager _playerManager;
        private IEnemiesManager _enemiesManager;

        public event Action OnStartBattle;
        public event Action OnEndBattle;
        
        [Inject]
        private void Construct(ITimeController timeController, IPlayerManager playerManager, IEnemiesManager enemiesManager)
        {
            _timeController = timeController;
            _playerManager = playerManager;
            _enemiesManager = enemiesManager;

            _playerManager.Stats.OnPlayerDied += EndBattle;
        }
        
        public void Dispose()
        {
            _playerManager.Stats.OnPlayerDied -= EndBattle;
        }

        public void StartBattle()
        {
            _enemiesManager.Spawner.StartSpawning();
            _playerManager.EnableControls();
            _timeController.Resume();
            OnStartBattle?.Invoke();
        }

        public void EndBattle()
        {
            _timeController.Pause();
            _playerManager.DisableControls();
            _enemiesManager.Spawner.StopSpawning();
            OnEndBattle?.Invoke();
        }
        
        public void RestartBattle()
        {
            _enemiesManager.Spawner.ClearPools();
            _timeController.Resume();
            //StartBattle();
        }
    }
}