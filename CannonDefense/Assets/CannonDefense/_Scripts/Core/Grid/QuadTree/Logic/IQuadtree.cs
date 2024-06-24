using System.Collections.Generic;
using UnityEngine;

namespace GlassyCode.CannonDefense.Core.Grid.QuadTree.Logic
{
    public interface IQuadtree
    {
        int PreferredNumberOfElementsInNode { get; }
        int MinNodeSize { get; }
        void AddElement(IElement element);
        void RemoveElement(IElement element);
        void AddElements(IEnumerable<IElement> elements);
        void RemoveElements(IEnumerable<IElement> elements);
        void UpdateObjectPosition(IElement element);
        IEnumerable<IElement> GetElementsInRange(Vector2 searchCenter, int radius);
    }
}