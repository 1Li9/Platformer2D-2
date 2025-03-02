using UnityEngine;

public class Mover
{
    private Rigidbody2D _rigidbody;

    public Mover(IMoveble context) =>
        _rigidbody = context.Rigitbody;

    public void Move(float horizontalVelocity) =>
        _rigidbody.velocity = new Vector2(horizontalVelocity, _rigidbody.velocity.y);
}
