using System.Collections.Generic;

public class EnemyAttackState : State
{
    private Enemy _context;

    public EnemyAttackState(Enemy context)
    {
        _context = context;

        EnterConditions = new List<Parameter>()
        {
            new Parameter(nameof(ParametersData.Params.CanAttack), true)
        };
    }

    public override void Update() =>
        _context.Attacker.Attack(_context.Timer, _context.AttackCooldownTime);

    public override void Exit() { }
}
