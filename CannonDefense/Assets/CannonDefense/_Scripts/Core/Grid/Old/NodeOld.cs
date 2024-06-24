/*using System.Collections.Generic;
using UnityEngine;

namespace GlassyCode.CannonDefense.Core.Grid.QuadTree.Logic
{
    public struct NodeOld
    {
        public readonly HashSet<ISpatialObject> Objects;
        public readonly int Depth;
        public Rect Rect;
        public NodeOld[] Children;
        
        public bool CanSplit(IQuadtreeOld owner)
        {
            return Rect.width >= owner.MinNodeSize * 2 && Rect.height >= owner.MinNodeSize * 2;
        }

        public NodeOld(Rect rect, int depth)
        {
            Objects = new HashSet<ISpatialObject>();
            Children = new NodeOld[] {};
            Rect = rect;
            Depth = depth;
        }

        public void AddObject(IQuadtreeOld owner, ISpatialObject obj)
        {
            if (Children == null || Children.Length == 0)
            {
                if (Objects.Count + 1 >= owner.PreferredMaxObjectsPerNode && CanSplit(owner))
                {
                    SplitNode(owner);
                }
                else
                {
                    Objects.Add(obj);
                }
            }
        }

        public void SplitNode(IQuadtreeOld owner)
        {
            var halfWidth = Rect.width / 2f;
            var halfHeight = Rect.height / 2f;
            var newDepth = Depth + 1;

            Children = new NodeOld[]
            {
                new(new Rect(Rect.xMin, Rect.yMin, halfWidth, halfHeight), newDepth),
                new(new Rect(Rect.xMin + halfWidth, Rect.yMin, halfWidth, halfHeight), newDepth),
                new(new Rect(Rect.xMin, Rect.yMin + halfHeight, halfWidth, halfHeight), newDepth),
                new(new Rect(Rect.xMin + halfWidth, Rect.yMin + halfHeight, halfWidth, halfHeight), newDepth)
            };

            foreach (var obj in Objects)
            {
                AddObjectToChildren(owner, obj);
            }
        }

        public void AddObjectToChildren(IQuadtreeOld owner, ISpatialObject obj)
        {
            foreach (var child in Children)
            {
                if (child.Overlaps(obj.Rect))
                {
                    child.AddObject(owner, obj);
                }
            }
        }
        
        public void RemoveObject(ISpatialObject obj)
        {
            if (Objects.Contains(obj))
            {
                Objects.Remove(obj);
            }

            if (Children != null)
            {
                foreach (var child in Children)
                {
                    if (child.Rect.Contains(obj.Position))
                    {
                        child.RemoveObject(obj);
                    }
                }
            }
        }
        
        public bool Overlaps(Rect other)
        {
            return other.Overlaps(other);
        }

        public HashSet<ISpatialObject> FindObjectsInRange(Rect searchRect)
        {
            var objects = new HashSet<ISpatialObject>();
            
            if (Children == null || Children.Length == 0)
            {
                if (Objects == null || Objects.Count == 0) return objects;
                
                objects.UnionWith(Objects);
                return objects;
            }

            foreach (var child in Children)
            {
                if (child.Overlaps(searchRect))
                {
                    return child.FindObjectsInRange(searchRect);
                }
            }

            return objects;
        }
    }
}*/