using UnityEngine;

namespace Game.Scripts.InputSystem
{
    public sealed class KeyBoardInputSystem : GameInputSystem
    {
        private void Update()
        {
            MoveInput = -Input.GetAxisRaw("Horizontal");
            
            if (Input.GetButtonDown("Jump"))
            {
                OnJump?.Invoke();
            }
        }
    }
}