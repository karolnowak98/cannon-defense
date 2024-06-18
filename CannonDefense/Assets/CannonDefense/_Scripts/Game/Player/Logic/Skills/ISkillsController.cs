namespace GlassyCode.CannonDefense.Game.Player.Logic.Skills
{
    public interface ISkillsController
    {
        void Dispose();
        void Tick();
        void Enable();
        void Disable();
        void Reset();
    }
}