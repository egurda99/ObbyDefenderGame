using System;
using UnityEngine;

namespace ObbyDefender
{
    public interface IPlayerInput
    {
        event Action<Vector2> OnMove;
        event Action OnJump;
        event Action OnAttack;
    }
}
