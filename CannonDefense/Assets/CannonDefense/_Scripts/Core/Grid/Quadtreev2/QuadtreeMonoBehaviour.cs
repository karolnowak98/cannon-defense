using GlassyCode.CannonDefense.Core.Utility;
using UnityEngine;

namespace GlassyCode.CannonDefense.Core.Grid.Quadtreev2
{
    [RequireComponent(typeof(Collider))]
    public class QuadtreeMonoBehaviour : GlassyMonoBehaviour, IQuadtreeMono
    {
        [SerializeField] private int _depth;
        private Collider _collider;

        public NodeMono Root { get; private set; }
        private Rect _rect;
        
        private void Awake()
        {
            TryGetComponent(out _collider);
            _rect = _collider.bounds.GetRect();

            Debug.Log($"{_rect.width}");
            Debug.Log($"{_rect.height}");
            
            Root = new NodeMono(_rect, _depth);
        }
    }
}