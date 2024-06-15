using GlassyCode.CannonDefense.Core.Data;
using GlassyCode.CannonDefense.Core.Utility;
using UnityEngine;

namespace GlassyCode.CannonDefense.Core.Applications.Data
{
    [CreateAssetMenu(menuName = MenuNames.Configs + nameof(ApplicationConfig), fileName = nameof(ApplicationConfig))]
    public class ApplicationConfig : Config, IApplicationConfig
    {
        [SerializeField] private int _targetFps;

        public int TargetFps => _targetFps;
    }
}