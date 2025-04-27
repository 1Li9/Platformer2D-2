using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    private int _numberOfTouches = 0;

    public bool IsGrounded => _numberOfTouches > 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Ground _))
            _numberOfTouches++;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Ground _))
            _numberOfTouches--;
    }
}
