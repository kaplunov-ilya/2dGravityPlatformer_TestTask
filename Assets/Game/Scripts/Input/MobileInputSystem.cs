using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.InputSystem
{
    public sealed class MobileInputSystem : GameInputSystem
    {
        [SerializeField] private CustomButton _leftButton;
        [SerializeField] private CustomButton _rightButton;
        [SerializeField] private Button _jumpButton;

        private void Start()
        {
            _jumpButton.onClick.AddListener(Jump);
            
            _leftButton.OnDown += IncrementMove;
            _leftButton.OnUp += DecrementMove;
            
            _rightButton.OnDown += DecrementMove;
            _rightButton.OnUp += IncrementMove;
        }

        private void IncrementMove()
        {
            MoveInput += 1;
        }
        
        private void DecrementMove()
        {
            MoveInput -= 1;
        }

        private void Jump()
        {
            OnJump?.Invoke();
        }
        
        private void OnDestroy()
        {
            _jumpButton.onClick.RemoveAllListeners();
            
            _leftButton.OnUp -= IncrementMove;
            _leftButton.OnDown -= DecrementMove;
            
            _rightButton.OnUp -= DecrementMove;
            _rightButton.OnDown -= IncrementMove;
        }
    }
}