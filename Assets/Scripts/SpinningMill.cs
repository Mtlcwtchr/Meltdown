using UnityEngine;

public class SpinningMill : MonoBehaviour
{
    [SerializeField] private float spinVelocity;

    private void Update()
    {
        var angle = spinVelocity * Time.deltaTime;
        transform.Rotate(transform.up, angle);
    }
}
