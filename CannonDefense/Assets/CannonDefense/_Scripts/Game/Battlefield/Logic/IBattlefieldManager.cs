using System;

namespace GlassyCode.CannonDefense.Game.Battlefield.Logic
{
    public interface IBattlefieldManager
    {
        void StartBattle();
        void EndBattle();
        void RestartBattle();
    }
}