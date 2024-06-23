using System.Collections.Generic;
using UnityEngine;

namespace GlassyCode.CannonDefense.Core.Grid.Quadtreev2
{
    public struct NodeMono
    {
        private readonly Rect _rect;
        private readonly int _depth;
        private NodeMono[] _children;
        private HashSet<IElement> _elements;
        
        public NodeMono(Rect rect, int depth)
        {
            _rect = rect;
            _depth = depth;
            _children = new NodeMono[4];
            _elements = new HashSet<IElement>();
        }
        
        public bool CanSplit(IQuadtreeMono owner)
        {
            return false;
            //return _rect.width >= owner.MinNodeSize * 2 && _rect.height >= owner.MinNodeSize * 2;
        }

        public void AddElement(IElement element)
        {
        }

        public void SplitNode(IQuadtreeMono owner)
        {
            var halfWidth = _rect.width / 2f;
            var halfHeight = _rect.height / 2f;
            var newDepth = _depth + 1;
        }
    }
}