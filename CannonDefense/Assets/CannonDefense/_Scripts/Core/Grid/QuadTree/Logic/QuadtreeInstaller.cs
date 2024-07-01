using GlassyCode.CannonDefense.Core.Grid.QuadTree.Data;
using UnityEngine;
using Zenject;

namespace GlassyCode.CannonDefense.Core.Grid.QuadTree.Logic
{
    public class QuadtreeInstaller : MonoInstaller
    {
        [SerializeField] private QuadtreeConfig _config;
        [SerializeField] private Collider _planeCollider;
        
        public override void InstallBindings()
        {
            Container.Bind(typeof(Quadtree), typeof(IQuadtree))
                .To<Quadtree>()
                .AsSingle().WithArguments(_config, _planeCollider);
        }
    }
}