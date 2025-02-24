using System;
using UnityEngine;

public class Follower
{
    private MonoBehaviour _context;
    private CharacterFlipper _flipper;
    private float _speed;

    public Follower(MonoBehaviour context, CharacterFlipper flipper, float speed)
    {
        _context = context;
        _flipper = flipper;
        _speed = speed;
    }

    public void Follow(Target target, Action action, float doActionDistan�e)
    {
        float xTargetPosition = target.Position.x;
        float xDirection = xTargetPosition - _context.transform.position.x;

        _context.transform.position = Vector3.MoveTowards(_context.transform.position, new Vector3(xTargetPosition, _context.transform.position.y), Time.deltaTime * _speed);

        if (Mathf.Abs(xDirection) <= doActionDistan�e)
            action?.Invoke();

        if (xDirection < 0f & _flipper.IsTurnedToRight | xDirection > 0f & _flipper.IsTurnedToRight == false)
            _flipper.Flip();
    }

    public void Follow(Target target, float doActionDistan�e) =>
        Follow(target, delegate () { }, doActionDistan�e);
}
