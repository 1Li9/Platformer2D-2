using System;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private GroundChecker _groundChecker;
    [SerializeField] private Attacker _attacker;

    private Player _player;
    private Rigidbody2D _rigidbody;

    private void Awake() =>
        _player = GetComponent<Player>();

    private void Start() =>
        _rigidbody = _player.Rigitbody;

    private void OnEnable()
    {
        _player.Dead += UpdateDead;
        _attacker.Attacked += UpdateTriggerIsAtacked;

    }

    private void OnDisable()
    {
        _player.Dead -= UpdateDead;
        _attacker.Attacked += UpdateTriggerIsAtacked;

    }

    private void Update()
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
