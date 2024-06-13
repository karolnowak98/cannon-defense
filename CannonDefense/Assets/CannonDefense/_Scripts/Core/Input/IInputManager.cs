using System;
using UnityEngine;

namespace GlassyCode.CannonDefense.Core.Input
{
    public interface IInputManager
    {
        Vector2 MoveAxis { get; }
        event Action OnSpacePressed;
        event Action OnYPressed;
    }
}