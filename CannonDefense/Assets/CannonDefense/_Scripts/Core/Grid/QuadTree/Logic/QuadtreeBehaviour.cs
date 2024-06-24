using System.Collections.Generic;
using GlassyCode.CannonDefense.Core.Utility;
using UnityEngine;

namespace GlassyCode.CannonDefense.Core.Grid.QuadTree.Logic
{
    [RequireComponent(typeof(Collider))]
    public sealed class QuadtreeBehaviour : GlassyMonoBehaviour, IQuadtree
    {
        [field: SerializeField] public int PreferredNumberOfElementsInNode { get; private set; }
        [field: SerializeField] public int MinNodeSize { get; private set; }
        [field: SerializeField] public int Depth { get; private set; }
        
        private Node _root;
        
        private void Awake()
        {
            TryGetComponent(out Collider col);
            
            _root = new Node(col.bounds.GetXZRect(), Depth);
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
        
        public void UpdateObjectPosition(IQuadtreeElement quadtreeElement)
        {
            var node = GetNodeForElement(quadtreeElement);

            if (node == null)
            {
                return;
            }
            
            node.Value.RemoveElement(quadtreeElement);
            AddElement(quadtreeElement);
        }

        public HashSet<IQuadtreeElement> GetElementsInRange(Vector2 searchCenter, int radius)
        {
            var distance = radius * 2;
            var searchRect = new Rect(searchCenter.x - radius, searchCenter.y - radius, distance, distance);
            var elements = _root.FindElementsInRect(searchRect);
            elements.RemoveWhere(el => (searchCenter - el.Position).sqrMagnitude > distance);
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