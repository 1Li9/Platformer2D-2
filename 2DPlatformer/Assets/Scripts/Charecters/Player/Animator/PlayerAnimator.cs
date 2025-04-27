using System;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour, IAnimator
{
    [SerializeField] private Animator _animator;

    public void SetHorizontalSpeed(float speed) =>
        _animator.SetFloat(PlayerAnimatorData.Params.HorizontalSpeed, Math.Abs(speed));

    public void SetVerticalSpeed(float speed) =>
        _animator.SetFloat(PlayerAnimatorData.Params.VerticalVelocity, speed);

    public void SetIsGrounded(bool isGrounded) =>
        _animator.SetBool(PlayerAnimatorData.Params.IsGrounded, isGrounded);

    public void UpdateDeadTrigger() =>
        _animator.SetTrigger(PlayerAnimatorData.Params.Dead);

    public void UpdateIsAtackedTrigger() =>
        _animator.SetTrigger(PlayerAnimatorData.Params.IsAttacked);
}
