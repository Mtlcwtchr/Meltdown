using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveVelocity;
    [SerializeField] private float rotVelocity;

    [SerializeField] private float jumpStrength;
    [SerializeField] private float duckFactor;

    [SerializeField] private Rigidbody rigidbody;

    [SerializeField] private UnityEvent OnJump;

    private PlayerState _currentState;

    private void Awake()
    {
        _currentState = PlayerState.Normal;
    }

    private void OnDestroy()
    {
        
    }

    public void MoveForward()
    {
        var dir = transform.forward;
        MoveAxis(dir);
    }

    public void MoveBack()
    {
        var dir = -transform.forward;
        MoveAxis(dir);
    }

    public void LookRight()
    {
        var dir = transform.right;
        RotateAxis(dir);
    }

    public void LookLeft()
    {
        var dir = -transform.right;
        RotateAxis(dir);
    }

    public void Jump()
    {
        TrySetState(PlayerState.Jumping);
    }

    public void Duck()
    {
        TrySetState(PlayerState.Duck);
    }

    public void EndDuck()
    {
        if (_currentState == PlayerState.Duck)
        {
            TrySetState(PlayerState.Normal);
        }
    }

    private void MoveAxis(Vector3 axis)
    {
        var pow = moveVelocity * Time.deltaTime;
        transform.position += axis * pow;
    }

    private void RotateAxis(Vector3 axis)
    {
        var pow = rotVelocity * Time.deltaTime;
        var lookRotation = Quaternion.LookRotation(axis, transform.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, pow);
    }

    private void TrySetState(PlayerState state)
    {
        if (CanTransitState(_currentState, state))
        {
            ProceedStateTransition(_currentState, _currentState = state);
        }
    }

    private void ProceedStateTransition(PlayerState from, PlayerState to)
    {
        switch (from)
        {
            case PlayerState.Normal:
                break;
            case PlayerState.Jumping:
                break;
            case PlayerState.Duck:
                ProceedDuckingEnd();
                break;
        }
        
        switch (to)
        {
            case PlayerState.Normal:
                break;
            case PlayerState.Jumping:
                ProceedJumping();
                break;
            case PlayerState.Duck:
                ProceedDucking();
                break;
        }
    }

    private void ProceedJumping()
    {
        rigidbody.AddForce(transform.up * jumpStrength);
        OnJump?.Invoke();
    }

    private void ProceedDucking()
    {
        var localScale = transform.localScale;
        localScale.y *= duckFactor;
        transform.localScale = localScale;
    }
    
    private void ProceedDuckingEnd()
    {
        var localScale = transform.localScale;
        localScale.y /= duckFactor;
        transform.localScale = localScale;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_currentState == PlayerState.Jumping && 
            collision.collider.CompareTag("ground"))
        {
            TrySetState(PlayerState.Normal);
        }
    }

    private bool CanTransitState(PlayerState from, PlayerState to) =>
        from switch
        {
            PlayerState.Normal => true,
            PlayerState.Jumping => to == PlayerState.Normal,
            PlayerState.Duck => to == PlayerState.Normal,
            _ => false
        };

    private enum PlayerState
    {
        Normal,
        Jumping,
        Duck
    }
}
