using System.Collections.Generic;
using GlassyCode.CannonDefense.Core.Utility;
using UnityEngine;

namespace GlassyCode.CannonDefense.Core.Grid.Quadtreev2
{
    [RequireComponent(typeof(Collider))]
    public class QuadtreeMonoBehaviour : GlassyMonoBehaviour, IQuadtreeMono
    {
        [field: SerializeField] public int PreferredNumberOfElementsInNode { get; private set; }
        [field: SerializeField] public int MinNodeSize { get; private set; }
        [field: SerializeField] public int Depth { get; private set; }
        
        private NodeMono _root;
        
        private void Awake()
        {
            TryGetComponent(out Collider col);
            
            _root = new NodeMono(col.bounds.GetXZRect(), Depth);
        }

        public void AddElement(IElement element)
        {
            _root.AddElement(this, element);
        }
        
        public void RemoveElement(IElement element)
        {
            _root.RemoveElement(element);
        }

        public void AddElements(IEnumerable<IElement> elements)
        {
            foreach (var element in elements)
            {
                AddElement(element);
            }
        }
        
        public void RemoveElements(IEnumerable<IElement> elements)
        {
            foreach (var element in elements)
            {
                RemoveElement(element);
            }            
        }
        
        public void UpdateObjectPosition(IElement element)
        {
            var node = GetNodeForElement(element);

            if (node == null)
            {
                return;
            }
            
            node.Value.RemoveElement(element);
            AddElement(element);
        }

        public IEnumerable<IElement> GetElementsInRange(Vector2 searchCenter, int radius)
        {
            var distance = radius * 2;
            var searchRect = new Rect(searchCenter.x - radius, searchCenter.y - radius, distance, distance);
            var elements = _root.FindElementsInRect(searchRect);
            elements.RemoveWhere(el => (searchCenter - el.Position).sqrMagnitude > distance);
            return elements;
        }

        private NodeMono? GetNodeForElement(IElement element)
        {
            return GetNodeForElement(_root, element);
        }
        
        private NodeMono? GetNodeForElement(NodeMono currentNode, IElement element)
        {
            return currentNode.IsElementInRect(element) ? currentNode : currentNode.FindElementInChildren(currentNode, element);
        }
    }
}