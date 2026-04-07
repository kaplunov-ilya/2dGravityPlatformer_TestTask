using UnityEngine;

public class GravityController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private float _gravityStrength = 1;
    [SerializeField] private float _raycastDistance = 2f;
    [SerializeField] private float _rotateSpeed = 5f;
    [SerializeField] private float _groundCheckDistance = 0.3f;

    [SerializeField] private LayerMask _groundLayer;

    private readonly RaycastHit2D[] _hit2Ds = new RaycastHit2D[1];
    private readonly Vector2[] _vector2S = new Vector2[3];
    
    private Vector2 _currentUp  = Vector2.down;
    
    public bool IsGrounded { get; private set; }
    
    public Vector2 GetUpDirection() => -_currentUp;
    
    void FixedUpdate()
    {
        var hit = GetBestHit();
        
        if (hit.collider != null)
        {
            IsGrounded = hit.distance < _groundCheckDistance;
            
            _currentUp = -hit.normal;
        }
        else
        {
            IsGrounded = false;
        }
        
        Vector3 gravityForce = _currentUp * _gravityStrength;
        _rb.AddForce(gravityForce, (ForceMode2D)ForceMode.Acceleration);
        RotateTowardsGravity();
    }

    private RaycastHit2D GetBestHit()
    {
        RaycastHit2D best = default;
        float closestDistance = Mathf.Infinity;
        
        Vector3 rayOrigin = transform.position;
        
        _vector2S[0] = _currentUp;
        _vector2S[1] = Quaternion.Euler(0, 0, 45) * _currentUp;
        _vector2S[2] = Quaternion.Euler(0, 0, -45) * _currentUp;

        foreach (var vector2 in _vector2S)
        {
            Physics2D.RaycastNonAlloc(rayOrigin, vector2, _hit2Ds, _raycastDistance, _groundLayer);
            CheckAndUpdateBestHit(ref best, ref closestDistance, _hit2Ds[0]);
        }
        
        Debug.DrawRay(rayOrigin, _vector2S[0] * _raycastDistance, IsGrounded ? Color.green : Color.red);
        Debug.DrawRay(rayOrigin, _vector2S[1] * _raycastDistance, IsGrounded ? Color.green : Color.red);
        Debug.DrawRay(rayOrigin, _vector2S[2] * _raycastDistance, IsGrounded ? Color.green : Color.red);
        
        return best;
    } 
    
    private void CheckAndUpdateBestHit(ref RaycastHit2D best, ref float closestDist, RaycastHit2D newHit)
    {
        if (newHit.collider != null && newHit.distance < closestDist)
        {
            best = newHit;
            closestDist = newHit.distance;
        }
    }
    
    private void RotateTowardsGravity()
    {
        float targetAngle = Mathf.Atan2(_currentUp.y, _currentUp.x) * Mathf.Rad2Deg;
        targetAngle -= 90f;

        Quaternion targetRotation = Quaternion.Euler(0f, 0f, targetAngle);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotateSpeed * Time.fixedDeltaTime);
    }
}
