using System.Collections.Generic;
using ModestTree;
using UnityEngine;

namespace GlassyCode.CannonDefense.Core.Grid.QuadTree.Logic
{
    public struct Node
    {
        private readonly HashSet<IQuadtreeElement> _elements;
        private readonly Rect _rect;
        private readonly int _depth;
        private Node[] _childrenNodes;
        
        private bool HasAnyElement => !_elements.IsEmpty();
        private bool HasAnyChildNode => !_childrenNodes.IsEmpty();
        public bool IsElementInRect(IQuadtreeElement quadtreeElement) => _rect.Contains(quadtreeElement.Position);
        private bool HasElement(IQuadtreeElement quadtreeElement) => _elements.Contains(quadtreeElement);
        private bool IsNodeFull(IQuadtree owner) => _elements.Count + 1 >= owner.PreferredNumberOfElementsInNode;
        private bool CanSplit(IQuadtree owner) => _rect.width >= owner.MinNodeSize * 2 && _rect.height >= owner.MinNodeSize * 2;
        private bool Overlaps(Rect rect) => _rect.Overlaps(rect);
        
        public Node(Rect rect, int depth)
        {
            _rect = rect;
            _depth = depth;
            _childrenNodes = new Node[]{ };
            _elements = new HashSet<IQuadtreeElement>();
        }
        
        public void RemoveElement(IQuadtreeElement quadtreeElement)
        {
            if (HasElement(quadtreeElement))
            {
                Debug.Log("Usuwam enemy z node'a z recta o pozycji:" + _rect.position);
                _elements.Remove(quadtreeElement);
            }

            foreach (var node in _childrenNodes)
            {
                if (node.IsElementInRect(quadtreeElement))
                {
                    node.RemoveElement(quadtreeElement);
                }
            }
        }
        
        public void AddElement(IQuadtree owner, IQuadtreeElement quadtreeElement)
        {
            if (HasAnyChildNode)
            {
                return;
            }
            
            Debug.Log("Dodaje nowego enemy w pozycji: " + quadtreeElement.Position);
            
            if (IsNodeFull(owner) && CanSplit(owner))
            {
                SplitNode(owner);
            }
            else
            {
                _elements.Add(quadtreeElement);
            }
        }
        
        public HashSet<IQuadtreeElement> FindElementsInRect(Rect searchRect)
        {
            var elements = new HashSet<IQuadtreeElement>();
            
            if (!HasAnyChildNode)
            {
                if (!HasAnyElement)
                {
                    return elements;
                }
                
                elements.UnionWith(_elements);
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
        
        public Node FindElementInChildren(Node currentNode, IQuadtreeElement quadtreeElement)
        {
            foreach (var child in currentNode._childrenNodes)
            {
                if (child.IsElementInRect(quadtreeElement))
                {
                    return FindElementInChildren(child, quadtreeElement);
                }
            }

            return currentNode;
        }

        private void SplitNode(IQuadtree owner)
        {
            var halfWidth = _rect.width / 2f;
            var halfHeight = _rect.height / 2f;
            var newDepth = _depth + 1;
            
            _childrenNodes = new Node[]
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
        
        private void AddElementToChildren(IQuadtree owner, IQuadtreeElement quadtreeElement)
        {
            foreach (var child in _childrenNodes)
            {
                if (child.Overlaps(quadtreeElement.Rect))
                {
                    child.AddElement(owner, quadtreeElement);
                }
            }
        }
    }
}