using GlassyCode.CannonDefense.Core.Data;
using UnityEngine;

namespace GlassyCode.CannonDefense.Core.Applications.Data
{
    [CreateAssetMenu(menuName = "Configs/Application Config", fileName = "Application Config")]
    public class ApplicationConfig : Config, IApplicationConfig
    {
        [SerializeField] private int _targetFps;

        public int TargetFps => _targetFps;
    }
}