using UnityEngine;

namespace GlassyCode.CannonDefense.Game.Player.Logic.Shooting
{
    [RequireComponent(typeof(Rigidbody))]
    public class CannonBallMb : MonoBehaviour
    {
        public float speed = 10f;

        private Rigidbody _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            _rb.AddForce(Vector3.forward * speed, ForceMode.Impulse);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                Destroy(gameObject);
            }
        }
    }
}