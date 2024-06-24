using System.Collections.Generic;
using ModestTree;
using UnityEngine;

namespace GlassyCode.CannonDefense.Core.Grid.Quadtreev2
{
    public struct NodeMono
    {
        private readonly HashSet<IElement> _elements;
        private readonly Rect _rect;
        private readonly int _depth;
        private NodeMono[] _childrenNodes;
        
        private bool HasAnyElement => !_elements.IsEmpty();
        private bool HasAnyChildNode => !_childrenNodes.IsEmpty();
        public bool IsElementInRect(IElement element) => _rect.Contains(element.Position);
        private bool HasElement(IElement element) => _elements.Contains(element);
        private bool IsNodeFull(IQuadtreeMono owner) => _elements.Count + 1 >= owner.PreferredNumberOfElementsInNode;
        private bool CanSplit(IQuadtreeMono owner) => _rect.width >= owner.MinNodeSize * 2 && _rect.height >= owner.MinNodeSize * 2;
        private bool Overlaps(Rect rect) => _rect.Overlaps(rect);
        
        public NodeMono(Rect rect, int depth)
        {
            _rect = rect;
            _depth = depth;
            _childrenNodes = new NodeMono[]{ };
            _elements = new HashSet<IElement>();
        }
        
        public void RemoveElement(IElement element)
        {
            if (HasElement(element))
            {
                _elements.Remove(element);
            }

            foreach (var node in _childrenNodes)
            {
                if (node.IsElementInRect(element))
                {
                    node.RemoveElement(element);
                }
            }
        }
        
        public void AddElement(IQuadtreeMono owner, IElement element)
        {
            if (HasAnyChildNode)
            {
                return;
            }
            
            if (IsNodeFull(owner) && CanSplit(owner))
            {
                SplitNode(owner);
            }
            else
            {
                _elements.Add(element);
            }
        }
        
        public HashSet<IElement> FindElementsInRect(Rect searchRect)
        {
            var elements = new HashSet<IElement>();
            
            if (!HasAnyChildNode)
            {
                if (!HasAnyElement)
                {
                    return elements;
                }
                
                elements.UnionWith(elements);
                return elements;
            }

            foreach (var child in _childrenNodes)
            {
                if (child.Overlaps(searchRect))
                {
                    return child.FindElementsInRect(searchRect);
                }
            }

            return elements;
        }
        
        public NodeMono FindElementInChildren(NodeMono currentNode, IElement element)
        {
            foreach (var child in currentNode._childrenNodes)
            {
                if (child.IsElementInRect(element))
                {
                    return FindElementInChildren(child, element);
                }
            }

            return currentNode;
        }

        private void SplitNode(IQuadtreeMono owner)
        {
            var halfWidth = _rect.width / 2f;
            var halfHeight = _rect.height / 2f;
            var newDepth = _depth + 1;
            
            _childrenNodes = new NodeMono[]
            {
                new(new Rect(_rect.xMin, _rect.yMin, halfWidth, halfHeight), newDepth),
                new(new Rect(_rect.xMin + halfWidth, _rect.yMin, halfWidth, halfHeight), newDepth),
                new(new Rect(_rect.xMin, _rect.yMin + halfHeight, halfWidth, halfHeight), newDepth),
                new(new Rect(_rect.xMin + halfWidth, _rect.yMin + halfHeight, halfWidth, halfHeight), newDepth)
            };

            foreach (var element in _elements)
            {
                AddElementToChildren(owner, element);
            }
        }
        
        private void AddElementToChildren(IQuadtreeMono owner, IElement element)
        {
            foreach (var child in _childrenNodes)
            {
                if (child.Overlaps(element.Rect))
                {
                    child.AddElement(owner, element);
                }
            }
        }
    }
}