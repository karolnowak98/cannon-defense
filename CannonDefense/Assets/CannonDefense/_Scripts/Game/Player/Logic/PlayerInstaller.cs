using System;
using GlassyCode.CannonDefense.Game.Player.Data;
using UnityEngine;
using Zenject;

namespace GlassyCode.CannonDefense.Game.Player.Logic
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private PlayerConfig _playerConfig;
        [SerializeField] private Transform _player;
        [SerializeField] private Rigidbody _playerRb;
        
        public override void InstallBindings()
        {
            Container.Bind<IPlayerConfig>().To<PlayerConfig>().FromInstance(_playerConfig).AsSingle();
            
            Container.Bind(typeof(PlayerController), typeof(IPlayerController),
                    typeof(IDisposable), typeof(ITickable), typeof(IFixedTickable))
                .To<PlayerController>()
                .AsSingle()
                .WithArguments(_player, _playerRb);
        }
    }
}