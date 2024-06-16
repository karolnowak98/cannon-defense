using UnityEngine.Pool;

namespace GlassyCode.CannonDefense.Game.Player.Logic.CannonBall
{
    public interface ICannonBallPool
    {
        ObjectPool<CannonBall> Pool { get; }
    }
}