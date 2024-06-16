namespace GlassyCode.CannonDefense.Game.Player.Logic.Movement
{
    public interface IMovementController
    {
        void Dispose();
        void Tick();
        void FixedTick();
        void Enable();
        void Disable();
        void ResetPosition();
    }
}