using System;
using GlassyCode.CannonDefense.Game.Player.Data;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace GlassyCode.CannonDefense.Game.Player.Logic
{
    public class PlayerInstaller : MonoInstaller
    {
        [FormerlySerializedAs("_playerConfig")] [SerializeField] private PlayerConfigData _playerConfigData;
        [SerializeField] private Transform _player;
        [SerializeField] private Rigidbody _playerRb;
        
        public override void InstallBindings()
        {
            Container.Bind<IPlayerConfig>().To<PlayerConfigData>().FromInstance(_playerConfigData).AsSingle();
            
            Container.Bind(typeof(PlayerController), typeof(IPlayerController),
                    typeof(IDisposable), typeof(ITickable), typeof(IFixedTickable))
                .To<PlayerController>()
                .AsSingle()
                .WithArguments(_player, _playerRb);
        }
    }
}