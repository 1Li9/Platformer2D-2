using UnityEngine;

public class EnemyAnimator
{
    public EnemyAnimator(Enemy context, Animator animator)
    {
        context.Attacker.Attacked += () => animator.SetTrigger(EnemyAnimatorData.Params.IsAttacked);
        context.Dead += () => animator.SetTrigger(EnemyAnimatorData.Params.Dead);
    }
}
