using GlassyCode.CannonDefense.Core.Input;
using GlassyCode.CannonDefense.Game.Player.Data;
using UnityEngine;

namespace GlassyCode.CannonDefense.Game.Player.Logic.Movement
{
    public sealed class MovementController : IMovementController
    {
        private readonly IInputManager _inputManager;
        private readonly Rigidbody _rb;
        private readonly float _maxMoveSpeed;
        private readonly float _moveSpeed;
        
        private Vector3 _moveDirection;
        private bool _canMove;
        
        public MovementController(IInputManager inputManager, Rigidbody rb, MovementData data)
        {
            _inputManager = inputManager;
            _rb = rb;
            _rb.drag = data.DragForce;
            
            _maxMoveSpeed = data.MaxMoveSpeed;
            _moveSpeed = data.MoveSpeed;
        }

        public void Dispose()
        {
            DisableMovement();
        }

        public void Tick()
        {
            if (!_canMove) return;
            
            GetInput();
            LimitMoveSpeed();
        }

        public void FixedTick()
        {
            if (!_canMove) return;
            
            AddMoveForce();
        }
        
        public void EnableMovement()
        {
            _canMove = true;
        }

        public void DisableMovement()
        {
            _canMove = false;
        }
        
        private void GetInput()
        {
            _moveDirection = new Vector3(_inputManager.MoveAxis.x, 0f, 0f);
        }
        
        private void AddMoveForce()
        {
            _rb.AddForce(_moveDirection * _moveSpeed, ForceMode.Force);
        }

        private void LimitMoveSpeed()
        {
            var velocity = _rb.velocity;
            var xVelocity = _rb.velocity.x;
            
            if (Mathf.Abs(xVelocity) > _maxMoveSpeed)
            {
                _rb.velocity = new Vector3(Mathf.Sign(xVelocity) * _maxMoveSpeed, velocity.y, velocity.z);
            }
        }
    }
}