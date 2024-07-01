using GlassyCode.CannonDefense.Core.Data;
using GlassyCode.CannonDefense.Core.Utility;
using UnityEngine;

namespace GlassyCode.CannonDefense.Core.Grid.QuadTree.Data
{
    [CreateAssetMenu(menuName = MenuNames.Configs + nameof(QuadtreeConfig), fileName = nameof(QuadtreeConfig))]
    public class QuadtreeConfig : Config, IQuadtreeConfig
    {
        [field: SerializeField] public int Depth { get; private set; }
        [field: SerializeField] public int MinNodeSize { get; private set; }
        [field: SerializeField] public int PreferredMaxObjectsPerNode { get; private set; }
    }
}