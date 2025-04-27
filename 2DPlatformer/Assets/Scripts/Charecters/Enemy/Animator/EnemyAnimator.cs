using UnityEngine;

public class EnemyAnimator : MonoBehaviour, IAnimator
{
    [SerializeField] private Animator _animator;

    public void SetHorizontalSpeed(float speed)
    {
    }

    public void SetIsGrounded(bool isGrounded)
    {
    }

    public void SetVerticalSpeed(float speed)
    {
    }

    public void UpdateDeadTrigger()
    {
        _animator.SetTrigger(EnemyAnimatorData.Params.Dead);
    }

    public void UpdateIsAtackedTrigger()
    {
        _animator.SetTrigger(EnemyAnimatorData.Params.IsAttacked);
    }
}
