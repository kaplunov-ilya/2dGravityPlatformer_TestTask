using System;
using UnityEngine;

namespace Game.Scripts.InputSystem
{
    public abstract class GameInputSystem : MonoBehaviour
    {
        public float MoveInput { get; protected set; }

        public Action OnJump { get; set; }
    }
}