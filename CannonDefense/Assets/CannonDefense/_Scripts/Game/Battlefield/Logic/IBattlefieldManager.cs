using System;

namespace GlassyCode.CannonDefense.Game.Battlefield.Logic
{
    public interface IBattlefieldManager
    {
        event Action OnStartBattle;
        event Action OnEndBattle;
        void StartBattle();
        void EndBattle();
        void RestartBattle();
    }
}