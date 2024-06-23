using System.Collections.Generic;
using UnityEngine;

namespace GlassyCode.CannonDefense.Core.Grid.QuadTree.Logic
{
    public interface IQuadtree
    {
        Node Root { get; }
        int MinNodeSize { get; }
        int PreferredMaxObjectsPerNode { get; }
        void AddObject(ISpatialObject obj);
        void AddObjects(IEnumerable<ISpatialObject> objects);
        IEnumerable<ISpatialObject> FindObjectsInRange(Vector2 searchLocation, int radius);
        void UpdateObjectPosition(ISpatialObject obj);
    }
}