using UnityEngine;
using UnityEngine.Events;

public class PlayerInputListener: MonoBehaviour
{
    [SerializeField] private UnityEvent OnMoveForward;
    [SerializeField] private UnityEvent OnMoveBack;
    [SerializeField] private UnityEvent OnMoveRight;
    [SerializeField] private UnityEvent OnMoveLeft;
    [SerializeField] private UnityEvent OnJump;
    [SerializeField] private UnityEvent OnDuck;
    [SerializeField] private UnityEvent OnDuckEnd;

    private Vector3 _lastMousePosition;

    private void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            OnMoveForward?.Invoke();
        }
        else if (Input.GetKey(KeyCode.S))
        {
            OnMoveBack?.Invoke();
        }

        if (Input.GetKey(KeyCode.D))
        {
            OnMoveRight?.Invoke();
        } 
        else if (Input.GetKey(KeyCode.A))
        {
            OnMoveLeft?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnDuckEnd?.Invoke();
            OnJump?.Invoke();
        } 
        else if (Input.GetKeyDown(KeyCode.C))
        {
            OnDuck?.Invoke();
        } 
        else if (Input.GetKeyUp(KeyCode.C))
        {
            OnDuckEnd?.Invoke();
        }
    }
}