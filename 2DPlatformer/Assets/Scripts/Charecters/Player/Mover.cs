using UnityEngine;

public class Mover
{
    private Rigidbody2D _rigidbody;
    private IAnimator _animator;

    public Mover(IMoveble context, IAnimator animator)
    {
        _rigidbody = context.Rigidbody;
        _animator = animator;
    }

    public void Move(float horizontalVelocity)
    {
        _rigidbody.velocity = new Vector2(horizontalVelocity, _rigidbody.velocity.y);
        _animator.SetHorizontalSpeed(horizontalVelocity);
    }
}
