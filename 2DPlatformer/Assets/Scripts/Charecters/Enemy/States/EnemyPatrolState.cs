using System.Collections.Generic;

public class EnemyPatrolState : State
{
    private const float ChangeTarget = .2f;

    private Enemy _context;

    public EnemyPatrolState(Enemy context)
    {
        _context = context;

        EnterConditions = new List<Parameter>()
        {
            new Parameter(nameof(ParametersData.Params.IsPlayerSpotted), false)
        };
    }

    public override void Update()
    {
        TargetsMap targetsMap = _context.TargetsMap;

        _context.Follower.Follow(targetsMap.CurrentTarget, () => targetsMap.SelectNextTarget(), ChangeTarget);
    }

    public override void Exit() { }
}
