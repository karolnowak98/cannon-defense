using System;
using GlassyCode.CannonDefense.Game.Player.Data;
using UnityEngine;
using Zenject;

namespace GlassyCode.CannonDefense.Game.Player.Logic
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private PlayerConfig _config;
        [SerializeField] private Transform _transform;
        [SerializeField] private Rigidbody _rb;
        [SerializeField] private Transform _cannonBallSpawnPoint;
        
        public override void InstallBindings()
        {
            Container.Bind<IPlayerConfig>().To<PlayerConfig>().FromInstance(_config).AsSingle();
            
            Container.Bind(typeof(PlayerManager), typeof(IPlayerManager),
                    typeof(IDisposable), typeof(ITickable), typeof(IFixedTickable))
                .To<PlayerManager>()
                .AsSingle()
                .WithArguments(_transform, _rb, _cannonBallSpawnPoint);
        }
    }
}