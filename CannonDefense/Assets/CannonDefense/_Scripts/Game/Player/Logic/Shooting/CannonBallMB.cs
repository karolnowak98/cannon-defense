using UnityEngine;

namespace GlassyCode.CannonDefense.Game.Player.Logic.Shooting
{
    [RequireComponent(typeof(Rigidbody))]
    public class CannonBallMb : MonoBehaviour
    {
        [SerializeField] private float _speed = 10f;

        private Rigidbody _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            _rb.velocity = Vector3.forward * _speed;
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