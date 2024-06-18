using System;
using GlassyCode.CannonDefense.Game.Player.Data;
using GlassyCode.CannonDefense.Game.Player.Logic.Shooting;
using GlassyCode.CannonDefense.Game.Player.Logic.Signals;
using GlassyCode.CannonDefense.Game.Player.Logic.Skills;
using GlassyCode.CannonDefense.Game.Player.Logic.Skills.Signals;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace GlassyCode.CannonDefense.Game.Player.Logic
{
    public sealed class PlayerInstaller : MonoInstaller
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

            DeclareSignals();
            BindFactories();
        }

        private void DeclareSignals()
        {
            Container.DeclareSignal<PlayerLeveledUpSignal>();
            Container.DeclareSignal<PlayerDiedSignal>();
            Container.DeclareSignal<PlayerStatsResetSignal>();
            Container.DeclareSignal<PlayerScoreUpdatedSignal>();
            Container.DeclareSignal<PlayerResetSignal>();
            
            Container.DeclareSignal<SkillProjectileBoomedSignal>();
            Container.DeclareSignal<SkillCooldownUpdatedSignal>();
            Container.DeclareSignal<SkillCooldownExpiredSignal>();
            Container.DeclareSignal<SkillUsedSignal>();
        }

        private void BindFactories()
        {
            Container.BindFactory<Object, CannonBall, CannonBall.Factory>().FromFactory<PrefabFactory<CannonBall>>();
            Container.BindFactory<Object, OffensiveSkillProjectile, OffensiveSkillProjectile.Factory>().FromFactory<PrefabFactory<OffensiveSkillProjectile>>();
        }
    }
}