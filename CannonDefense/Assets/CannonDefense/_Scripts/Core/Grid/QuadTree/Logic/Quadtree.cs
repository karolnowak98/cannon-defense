using System.Collections.Generic;
using GlassyCode.CannonDefense.Core.Grid.QuadTree.Data;
using UnityEngine;
using Zenject;

namespace GlassyCode.CannonDefense.Core.Grid.QuadTree.Logic
{
    public sealed class Quadtree : IQuadtree
    {
        public Node Root { get; private set; }
        public int MinNodeSize { get; private set; }
        public int PreferredMaxObjectsPerNode { get; private set; }

        [Inject]
        private void Construct(IQuadtreeConfig config, Collider collider)
        {
            MinNodeSize = config.MinNodeSize;
            PreferredMaxObjectsPerNode = config.PreferredMaxObjectsPerNode;
            
            Init(config.Depth, collider.bounds);
        }

        public void Init(int depth, Bounds bounds)
        {
            var rect = new Rect(bounds.min.x, bounds.min.z, bounds.size.x, bounds.size.z);
            Root = new Node(rect, depth);
        }

        public void AddObject(ISpatialObject obj)
        {
            Root.AddObject(this, obj);
        }
        
        public void RemoveObject(ISpatialObject obj)
        {
            Root.RemoveObject(obj);
        }

        public void AddObjects(IEnumerable<ISpatialObject> objects)
        {
            foreach (var obj in objects)
            {
                AddObject(obj);
            }
        }

        public IEnumerable<ISpatialObject> FindObjectsInRange(Vector2 searchLocation, int radius)
        {
            var distance = radius * 2;
            var searchRect = new Rect(searchLocation.x - radius, searchLocation.y - radius, distance, distance);
            var objects = Root.FindObjectsInRange(searchRect);
            objects.RemoveWhere(x => (searchLocation - x.Position).sqrMagnitude > distance);
            return objects;
        }
        
        public Node? FindNodeForObject(ISpatialObject obj)
        {
            return FindNodeForObject(Root, obj);
        }

        private Node? FindNodeForObject(Node currentNode, ISpatialObject obj)
        {
            if (currentNode.Rect.Contains(obj.Position))
            {
                return currentNode;
            }
        
            if (currentNode.Children != null)
            {
                foreach (var child in currentNode.Children)
                {
                    if (child.Rect.Contains(obj.Position))
                    {
                        return FindNodeForObject(child, obj);
                    }
                }
            }
        
            return currentNode; 
        }

        public void UpdateObjectPosition(ISpatialObject obj)
        {
            var node = FindNodeForObject(obj);
            if (node != null)
            {
                node.Value.RemoveObject(obj);
                AddObject(obj);
            }
        }
    }
}