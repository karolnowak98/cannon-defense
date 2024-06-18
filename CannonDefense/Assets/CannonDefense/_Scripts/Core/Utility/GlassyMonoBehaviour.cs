using UnityEngine;

namespace GlassyCode.CannonDefense.Core.Utility
{
    public abstract class GlassyMonoBehaviour : MonoBehaviour
    {
        public bool IsActive => gameObject.activeSelf;
        public Transform Transform => transform; 
        
        public void Enable()
        {
            gameObject.SetActive(true);
        }
        
        public void Disable()
        {
            gameObject.SetActive(false);
        }

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }
        
        public void SetRotation(Quaternion rotation)
        {
            transform.rotation = rotation;
        }

        public virtual void Destroy()
        {
            if (IsActive)
            {
                Destroy(gameObject);
            }
        }

        public void SetParent(Transform parent)
        {
            if (IsActive)
            {
                transform.parent = parent;
            }
        }
        
        public void DestroyImmediate()
        {
            DestroyImmediate(gameObject);
        }

        public void Create()
        {
            Instantiate(this);
        }
    }
}