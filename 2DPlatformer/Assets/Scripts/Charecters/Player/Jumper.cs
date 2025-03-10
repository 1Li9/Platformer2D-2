using UnityEngine;

public class Jumper
{
    private Rigidbody2D _rigidbody;

    public Jumper(IMoveble context) =>
        _rigidbody = context.Rigitbody;

    public void Jump(float jumpForce) =>
        _rigidbody.AddForce(Vector2.up * jumpForce);
}
