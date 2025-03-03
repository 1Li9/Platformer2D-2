using UnityEngine;
public class EnemyAttackState : IState
{
    public IState Update(Enemy enemy)
    {
        if (enemy.Parameters.Get(ParametersData.Params.CanAttack).Value == false)
        {
            Exit(enemy);

            return new EnemyFollowState();
        }

        enemy.Attacker.Attack(enemy.Timer, enemy.AttackCooldownTime);

        return this;
    }

    public void Exit(Enemy enemy)
    {
    }
}
