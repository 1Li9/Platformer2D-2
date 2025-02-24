using UnityEngine;

public static class PlayerAnimatorData
{
    public static class Params
    {
        public static readonly int IsGrounded = Animator.StringToHash(nameof(IsGrounded));
        public static readonly int Dead = Animator.StringToHash(nameof(Dead));
        public static readonly int IsJumped = Animator.StringToHash(nameof(IsJumped));
        public static readonly int VerticalVelocity = Animator.StringToHash(nameof(VerticalVelocity));
        public static readonly int HorizontalSpeed = Animator.StringToHash(nameof(HorizontalSpeed));
    }
}
