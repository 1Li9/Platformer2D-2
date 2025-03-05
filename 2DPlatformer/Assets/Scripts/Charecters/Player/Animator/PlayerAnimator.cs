using System;
using UnityEngine;

public class PlayerAnimator
{
    private Animator _animator;
    private GroundChecker _groundChecker;
    private Attacker _attacker;

    private Rigidbody2D _rigidbody;

    public PlayerAnimator(Player context, Animator animator, GroundChecker groundChecker, Attacker attacker)
    {
        _animator = animator;
        _groundChecker = groundChecker;
        _attacker = attacker;
        _rigidbody = context.Rigitbody;

        context.Dead += UpdateDead;
        _attacker.Attacked += UpdateTriggerIsAtacked;
    }

    public void Update()
    {
        UpdateHorizontalSpeed();
        UpdateVerticalVelocity();
        UpdateIsGrounded();
    }

    private void UpdateHorizontalSpeed() =>
        _animator.SetFloat(PlayerAnimatorData.Params.HorizontalSpeed, Math.Abs(_rigidbody.velocity.x));

    private void UpdateVerticalVelocity()
    {
        float verticalVelocity = _rigidbody.velocity.y;
        _animator.SetFloat(PlayerAnimatorData.Params.VerticalVelocity, verticalVelocity);

        if (_animator.GetBool(PlayerAnimatorData.Params.IsJumped))
            _animator.SetBool(PlayerAnimatorData.Params.IsJumped, true);
    }

    private void UpdateIsGrounded()
    {
        _animator.SetBool(PlayerAnimatorData.Params.IsGrounded, _groundChecker.IsGrounded);

        if (_groundChecker.IsGrounded)
            _animator.SetBool(PlayerAnimatorData.Params.IsJumped, false);
    }

    private void UpdateDead() =>
        _animator.SetTrigger(PlayerAnimatorData.Params.Dead);

    private void UpdateTriggerIsAtacked() =>
        _animator.SetTrigger(PlayerAnimatorData.Params.IsAttacked);
}
