using GlassyCode.CannonDefense.Core.Data;
using GlassyCode.CannonDefense.Core.Utility;
using UnityEngine;

namespace GlassyCode.CannonDefense.Core.Applications.Data
{
    [CreateAssetMenu(menuName = MenuNames.Configs + nameof(ApplicationConfigData), fileName = nameof(ApplicationConfigData))]
    public class ApplicationConfigData : ConfigData, IApplicationConfig
    {
        [SerializeField] private int _targetFps;

        public int TargetFps => _targetFps;
    }
}