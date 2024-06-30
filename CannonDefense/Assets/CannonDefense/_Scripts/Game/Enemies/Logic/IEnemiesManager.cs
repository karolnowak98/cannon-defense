using GlassyCode.CannonDefense.Core.Grid.QuadTree.Logic;

namespace GlassyCode.CannonDefense.Game.Enemies.Logic
{
    public interface IEnemiesManager
    {
        IQuadtree Quadtree { get; }
        IEnemySpawner Spawner { get; }
    }
}