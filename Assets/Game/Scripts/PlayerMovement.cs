using Game.Scripts.InputSystem;
using UnityEngine;

namespace Game.Scripts
{
    public sealed class PlayerMovement  : MonoBehaviour
    {
        [SerializeField] private GameInputSystem _gameInputSystem;
        
        [SerializeField] private Rigidbody2D _rb;
        [SerializeField] private GravityController _gravityController;

        [Header("Movement")]
        [SerializeField] private float _moveSpeed = 5f;
        [SerializeField] private float _jumpForce = 10f;

        private float MoveInput => _gameInputSystem.MoveInput;
        
        private void Start()
        {
            _gameInputSystem.OnJump += Jump;
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void Move()
        {
            if (Mathf.Abs(MoveInput) < 0.01f)
            {
                return;
            }

            Vector2 moveDirection = transform.right;
            Vector2 moveForce = moveDirection * (MoveInput * _moveSpeed);
            
            _rb.AddForce(moveForce, (ForceMode2D)ForceMode.Acceleration);
        }

        private void Jump()
        {
            if (!_gravityController.IsGrounded) 
                return;

            _rb.AddForce(_gravityController.GetUpDirection() * _jumpForce, ForceMode2D.Impulse);
        }

        private void OnDestroy()
        {
            _gameInputSystem.OnJump -= Jump;
        }
    }
}