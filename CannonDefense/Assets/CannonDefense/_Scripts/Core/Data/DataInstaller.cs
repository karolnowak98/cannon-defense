using UnityEngine;
using Zenject;

namespace GlassyCode.CannonDefense.Core.Data
{
    public class DataInstaller : MonoInstaller
    {
        [SerializeField] private DataHolder _dataHolder;
        
        public override void InstallBindings()
        {
            Container.Bind(typeof(IDataProvider), typeof(DataProvider))
                .To<DataProvider>()
                .AsSingle()
                .WithArguments(_dataHolder)
                .NonLazy();
        }
    }
}