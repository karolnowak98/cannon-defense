using System;
using UnityEngine;
using Zenject;

namespace GlassyCode.CannonDefense.Core.Input
{
    public sealed class InputManager : IInputManager, IInitializable, IDisposable
    {
        private Controls _controls;
        
        public Vector2 MoveAxis { get; private set; }

        public event Action OnSpacePressed;
        public event Action OnYPressed;

        public void Initialize()
        {
            _controls = new Controls();
            
            _controls.Cannon.Shoot.performed += _ => OnSpacePressed?.Invoke();
            _controls.Cannon.UseSkill.performed += _ => OnYPressed?.Invoke();
            _controls.Cannon.Move.performed += x => MoveAxis = x.ReadValue<Vector2>();
            _controls.Cannon.Move.canceled += _ =>  MoveAxis = Vector2.zero;

            _controls.Enable();
        }

        public void Dispose()
        {
            _controls.Disable();
        }
    }
}