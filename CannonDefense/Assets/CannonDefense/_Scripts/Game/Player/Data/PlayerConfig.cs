using GlassyCode.CannonDefense.Core.Data;
using GlassyCode.CannonDefense.Core.Utility;
using UnityEngine;

namespace GlassyCode.CannonDefense.Game.Player.Data
{
    [CreateAssetMenu(menuName = MenuNames.Configs + nameof(PlayerConfig), fileName = nameof(PlayerConfig))]
    public class PlayerConfig : Config, IPlayerConfig
    {
        
    }
}