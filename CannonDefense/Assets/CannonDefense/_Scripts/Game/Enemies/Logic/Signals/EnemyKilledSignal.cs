using GlassyCode.CannonDefense.Game.Enemies.Data;

namespace GlassyCode.CannonDefense.Game.Enemies.Logic.Signals
{
    public struct EnemyKilledSignal
    {
        public EnemyType Type;
        public int Score;
        public int Experience;
    }
}