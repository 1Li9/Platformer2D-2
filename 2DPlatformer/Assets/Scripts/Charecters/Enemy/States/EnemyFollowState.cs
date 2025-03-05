using System.Collections.Generic;

public class EnemyFollowState : State
{
    private Enemy _context;

    public EnemyFollowState(Enemy context)
    {
        _context = context;

        EnterConditions = new List<Parameter>()
        {
            new Parameter(nameof(ParametersData.Params.IsPlayerSpotted), true)
        };
    }

    public override void Update() =>
        _context.Follower.Follow(_context.PlayerTarget);

    public override void Exit() { }
}
