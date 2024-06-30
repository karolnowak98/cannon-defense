using System.Collections.Generic;
using GlassyCode.CannonDefense.Core.Grid.QuadTree.Data;
using UnityEngine;
using Zenject;

namespace GlassyCode.CannonDefense.Core.Grid.QuadTree.Logic
{
    public sealed class Quadtree : IQuadtree
    {
        public int PreferredNumberOfElementsInNode { get; private set; }
        public int MinNodeSize { get; private set; }
        public int Depth { get; private set; }

        private Node _root;
        private Bounds _bounds;

        [Inject]
        private void Construct(IQuadtreeConfig config, Collider collider)
        {
            Depth = config.Depth;
            MinNodeSize = config.MinNodeSize;
            PreferredNumberOfElementsInNode = config.PreferredMaxObjectsPerNode;
            _bounds = collider.bounds;
        }

        public void AddElement(IQuadtreeElement quadtreeElement)
        {
            _root.AddElement(this, quadtreeElement);
        }
        
        public void RemoveElement(IQuadtreeElement quadtreeElement)
        {
            _root.RemoveElement(quadtreeElement);
        }

        public void AddElements(IEnumerable<IQuadtreeElement> elements)
        {
            foreach (var element in elements)
            {
                AddElement(element);
            }
        }
        
        public void RemoveElements(IEnumerable<IQuadtreeElement> elements)
        {
            foreach (var element in elements)
            {
                RemoveElement(element);
            }            
        }
        
        public void UpdateElementNode(IQuadtreeElement quadtreeElement)
        {
            var currentNode = GetNodeForElement(quadtreeElement);
            
            if (currentNode == null || currentNode.IsElementInRect(quadtreeElement))
            {
                return;
            }

            currentNode.RemoveElement(quadtreeElement);
            AddElement(quadtreeElement);
        }
        
        public IEnumerable<IQuadtreeElement> GetElementsInRange(Vector2 searchCenter, float radius)
        {
            var foundElements = new HashSet<IQuadtreeElement>();
            var distance = radius * 2;
            var searchRect = new Rect(searchCenter.x - radius, searchCenter.y - radius, distance, distance);
            _root.FindElementsInRect(searchRect, foundElements);
            foundElements.RemoveWhere(el => (searchCenter - el.Position).sqrMagnitude > radius * radius);
            return foundElements;
        }

        public HashSet<IQuadtreeElement> GetAllElements()
        {
            var elements = new HashSet<IQuadtreeElement>();
            _root.GetAllElements(elements);
            return elements;
        }

        public void Reset()
        {
            _root = new Node(new Rect(_bounds.min.x, _bounds.min.z, _bounds.size.x, _bounds.size.z), Depth);
        }
        
        private Node GetNodeForElement(IQuadtreeElement quadtreeElement)
        {
            return GetNodeForElement(_root, quadtreeElement);
        }
        
        private Node GetNodeForElement(Node currentNode, IQuadtreeElement quadtreeElement)
        {
            return currentNode.IsElementInRect(quadtreeElement) ? currentNode : currentNode.FindElementInChildren(currentNode, quadtreeElement);
        }
    }
}