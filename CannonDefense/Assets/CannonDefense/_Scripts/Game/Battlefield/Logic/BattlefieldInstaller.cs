using System;
using Zenject;

namespace GlassyCode.CannonDefense.Game.Battlefield.Logic
{
    public sealed class BattlefieldInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind(typeof(BattlefieldManager), typeof(IBattlefieldManager), typeof(IDisposable))
                .To<BattlefieldManager>()
                .AsSingle();
        }
    }
}