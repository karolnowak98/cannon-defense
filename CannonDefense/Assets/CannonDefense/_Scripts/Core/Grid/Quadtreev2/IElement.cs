using UnityEngine;

namespace GlassyCode.CannonDefense.Core.Grid.Quadtreev2
{
    public interface IElement
    {
        Vector2 Position { get; }
        Rect Rect { get; }
    }
}