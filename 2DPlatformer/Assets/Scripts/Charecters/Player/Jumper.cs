using UnityEngine;

public class Jumper
{
    private Rigidbody2D _rigidbody;
    private IAnimator _animator;

    public Jumper(IMoveble context, IAnimator animator)
    {
        _rigidbody = context.Rigitbody;
        _animator = animator;
    }

    public void Jump(float jumpForce)
    {
        _rigidbody.AddForce(Vector2.up * jumpForce);
        _animator.SetVerticalSpeed(_rigidbody.velocity.y);
    }
}
