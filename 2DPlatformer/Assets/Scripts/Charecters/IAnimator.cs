public interface IAnimator
{
    public void SetHorizontalSpeed(float speed);

    public void SetVerticalSpeed(float speed);

    public void SetIsGrounded(bool isGrounded);

    public void UpdateDeadTrigger();

    public void UpdateIsAtackedTrigger();
}
