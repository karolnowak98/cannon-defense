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

        private Node _root;

        [Inject]
        private void Construct(IQuadtreeConfig config, Collider collider)
        {
            var bounds = collider.bounds;
            
            MinNodeSize = config.MinNodeSize;
            PreferredNumberOfElementsInNode = config.PreferredMaxObjectsPerNode;
            _root = new Node(new Rect(bounds.min.x, bounds.min.z, bounds.size.x, bounds.size.z), config.Depth);
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
        
        public HashSet<IQuadtreeElement> GetElementsInRange(Vector2 searchCenter, int radius)
        {
            var distance = radius * 2;
            var searchRect = new Rect(searchCenter.x - radius, searchCenter.y - radius, distance, distance);
            var elements = _root.FindElementsInRect(searchRect);
            elements.RemoveWhere(el => (searchCenter - el.Position).sqrMagnitude > (radius * radius));
            return elements;
        }
        
        private Node? GetNodeForElement(IQuadtreeElement quadtreeElement)
        {
            return GetNodeForElement(_root, quadtreeElement);
        }
        
        private Node? GetNodeForElement(Node currentNode, IQuadtreeElement quadtreeElement)
        {
            return currentNode.IsElementInRect(quadtreeElement) ? currentNode : currentNode.FindElementInChildren(currentNode, quadtreeElement);
        }
    }
}