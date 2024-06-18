using System;
using GlassyCode.CannonDefense.Core.Time;
using GlassyCode.CannonDefense.Game.Enemies.Logic;
using GlassyCode.CannonDefense.Game.Player.Logic;
using GlassyCode.CannonDefense.Game.Player.Logic.Signals;
using Zenject;

namespace GlassyCode.CannonDefense.Game.Battlefield.Logic
{
    public sealed class BattlefieldManager : IBattlefieldManager, IDisposable
    {
        private SignalBus _signalBus;
        private ITimeController _timeController;
        private IPlayerManager _playerManager;
        private IEnemiesManager _enemiesManager;

        public event Action OnStartBattle;
        public event Action OnEndBattle;
        
        [Inject]
        private void Construct(SignalBus signalBus, ITimeController timeController, IPlayerManager playerManager, IEnemiesManager enemiesManager)
        {
            _signalBus = signalBus;
            _timeController = timeController;
            _playerManager = playerManager;
            _enemiesManager = enemiesManager;

            _signalBus.Subscribe<PlayerDiedSignal>(EndBattle);
        }
        
        public void Dispose()
        {
            _signalBus.TryUnsubscribe<PlayerDiedSignal>(EndBattle);
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
            _enemiesManager.Spawner.RemoveEnemies();
            _playerManager.Reset();
            StartBattle();
        }
    }
}