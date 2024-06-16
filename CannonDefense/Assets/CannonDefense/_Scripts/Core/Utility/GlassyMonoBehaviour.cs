using UnityEngine;

namespace GlassyCode.CannonDefense.Core.Utility
{
    public abstract class GlassyMonoBehaviour : MonoBehaviour
    {
        public bool IsActive => gameObject.activeSelf;
        
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

        public void Destroy()
        {
            Destroy(gameObject);
        }

        public void Create()
        {
            Instantiate(this);
        }
    }
}