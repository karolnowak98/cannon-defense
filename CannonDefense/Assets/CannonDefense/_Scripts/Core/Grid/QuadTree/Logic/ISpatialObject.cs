using UnityEngine;

namespace GlassyCode.CannonDefense.Core.Grid.QuadTree.Logic
{
    public interface ISpatialObject
    {
        Vector3 Position { get; }
        Rect Rect { get; }
    }
}