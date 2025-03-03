using UnityEngine;
public static class EnemyAnimatorData
{
    public static class Params
    {
        public static readonly int IsAttacked = Animator.StringToHash(nameof(IsAttacked));
        public static readonly int Dead = Animator.StringToHash(nameof(Dead));
    }
}
