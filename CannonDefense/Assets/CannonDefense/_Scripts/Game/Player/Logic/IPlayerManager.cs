using GlassyCode.CannonDefense.Game.Player.Logic.Stats;

namespace GlassyCode.CannonDefense.Game.Player.Logic
{
    public interface IPlayerManager
    {
        IStatsController Stats { get; }
        void EnableControls();
        void DisableControls();
    }
}