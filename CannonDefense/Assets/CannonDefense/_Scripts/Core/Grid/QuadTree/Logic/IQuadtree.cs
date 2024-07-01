using System.Collections.Generic;
using UnityEngine;

namespace GlassyCode.CannonDefense.Core.Grid.QuadTree.Logic
{
    public interface IQuadtree
    {
        int PreferredNumberOfElementsInNode { get; }
        int MinNodeSize { get; }
        void AddElement(IQuadtreeElement quadtreeElement);
        void RemoveElement(IQuadtreeElement quadtreeElement);
        void AddElements(IEnumerable<IQuadtreeElement> elements);
        void RemoveElements(IEnumerable<IQuadtreeElement> elements);
        void UpdateElementNode(IQuadtreeElement quadtreeElement);
        IEnumerable<IQuadtreeElement> GetElementsInRange(Vector2 searchCenter, float radius);
        HashSet<IQuadtreeElement> GetAllElements();
        void Reset();
    }
}