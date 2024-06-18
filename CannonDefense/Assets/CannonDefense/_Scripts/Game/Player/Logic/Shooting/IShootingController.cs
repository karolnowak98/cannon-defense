namespace GlassyCode.CannonDefense.Game.Player.Logic.Shooting
{
    public interface IShootingController
    {
        void Dispose();
        void Enable();
        void Disable();
        void ResetCannonBalls();
    }
}