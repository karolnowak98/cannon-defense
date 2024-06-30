using System.Collections.Generic;
using ModestTree;
using UnityEngine;

namespace GlassyCode.CannonDefense.Core.Grid.QuadTree.Logic
{
    public class Node
    {
        private readonly Rect _rect;
        private readonly int _depth;
        private HashSet<IQuadtreeElement> _elements;
        private Node[] _childrenNodes;
        
        private bool HasAnyChildNode => !_childrenNodes.IsEmpty();
        public bool IsElementInRect(IQuadtreeElement quadtreeElement) => _rect.Contains(quadtreeElement.Position);
        private bool IsNodeFull(IQuadtree owner) => _elements.Count + 1 >= owner.PreferredNumberOfElementsInNode;
        private bool CanSplit(IQuadtree owner) => _rect.width >= owner.MinNodeSize * 2 && _rect.height >= owner.MinNodeSize * 2;
        private bool Overlaps(Rect rect) => _rect.Overlaps(rect);
        
        public Node(Rect rect, int depth)
        {
            _rect = rect;
            _depth = depth;
            _childrenNodes = new Node[]{ };
        }
        
        public void RemoveElement(IQuadtreeElement quadtreeElement)
        {
            var removed = _elements?.Remove(quadtreeElement);

            if (removed is true)
            {
                return;
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
                AddElementToChildren(owner, quadtreeElement);
            }
            else
            {
                _elements ??= new HashSet<IQuadtreeElement>();

                if (IsNodeFull(owner) && CanSplit(owner))
                {
                    SplitNode(owner);
                } 
                else
                {
                    _elements.Add(quadtreeElement);
                }
            }
        }
        
        public void FindElementsInRect(Rect searchRect, HashSet<IQuadtreeElement> foundElements)
        {
            if (HasAnyChildNode)
            {
                foreach (var child in _childrenNodes)
                {
                    if (child.Overlaps(searchRect))
                    {
                        child.FindElementsInRect(searchRect, foundElements);
                    }
                }
            }
            else
            {
                if (_elements == null)
                {
                    return;
                }

                foundElements.UnionWith(_elements);
            }
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

        public void GetAllElements(HashSet<IQuadtreeElement> elements)
        {
            if (HasAnyChildNode)
            {
                foreach (var child in _childrenNodes)
                {
                    child.GetAllElements(elements);
                }
            }
            else
            {
                if (_elements == null)
                {
                    return;
                }
                
                elements.UnionWith(_elements);
            }
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

            _elements = null;
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