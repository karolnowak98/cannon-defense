using Zenject;

namespace GlassyCode.CannonDefense.Game.Enemies.Logic
{
    public class EnemyFactory : IFactory<UnityEngine.Object, IEnemy>
    {
        private readonly DiContainer _container;
        
        public EnemyFactory(DiContainer container)
        {
            _container = container;
        }
        
        public IEnemy Create(UnityEngine.Object prefab)
        {
            return _container.InstantiatePrefabForComponent<Enemy>(prefab);
        }
    }
}