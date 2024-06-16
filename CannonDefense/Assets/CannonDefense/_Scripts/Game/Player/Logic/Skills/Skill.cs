namespace GlassyCode.CannonDefense.Game.Player.Logic.Skills
{
    public abstract class Skill : ISkill
    {
        protected ISkillEntityData Data { get; private set; }
        
        protected Skill(ISkillEntityData data)
        {
            Data = data;
        }

        public virtual void Enable()
        {
            Data.InputAction.Enable();
        }

        public virtual void Disable()
        {
            Data.InputAction.Disable();
        }

        public virtual void Use()
        {
            
        }
    }
}