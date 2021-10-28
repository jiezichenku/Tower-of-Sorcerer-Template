using System.Collections;
using System.Collections.Generic;

public class EnemyProperty
{
    //Using singleton pattern
    private static EnemyProperty singleIntance;
    public static EnemyProperty GetInstance()
    {
        if (singleIntance == null)
        {
            singleIntance = new EnemyProperty();
        }
        return singleIntance;
    }

    public int DamageCalculate(Enemy enemy)
    {
        BraverStatus braverStatus = BraverStatus.GetInstance();
        //Get braver attribute
        braverStatus.getAttributes().TryGetValue(BraverAttribute.Attack, out int braverAttack);
        braverStatus.getAttributes().TryGetValue(BraverAttribute.Defence, out int braverDefence);
        braverStatus.getAttributes().TryGetValue(BraverAttribute.Shield, out int braverShield);
        //Get enemy attribute
        enemy.getAttributes().TryGetValue(EnemyAttribute.Health, out int enemyHealth);
        enemy.getAttributes().TryGetValue(EnemyAttribute.Attack, out int enemyAttack);
        enemy.getAttributes().TryGetValue(EnemyAttribute.Defence, out int enemyDefence);
        List<EnemySkill> enemySkills = enemy.getSkills();

        //Skills affect on attributes
        int braverAttackDelta = 0;
        int braverDefenceDelta = 0;
        int braverShieldDelta = 0;
        double braverAttackRate = 1.0;
        double braverDefenceRate = 1.0;
        double braverShieldRate = 1.0;
        int enemyHealthDelta = 0;
        int enemyAttackDelta = 0;
        int enemyDefenceDelta = 0;
        double enemyHealthRate = 1.0;
        double enemyAttackRate = 1.0;
        double enemyDefenceRate = 1.0;
        
        //Skills effect here
        int affectedBraverAttack = (int)(braverAttack * braverAttackRate) + braverAttackDelta;
        int affectedBraverDefence = (int)(braverDefence * braverDefenceRate) + braverDefenceDelta;
        int affectedBraverShield = (int)(braverShield * braverShieldRate) + braverShieldDelta;
        int affectedEnemyHealth = (int)(enemyHealth * enemyHealthRate) + enemyHealthDelta;
        int affectedEnemyAttack = (int)(enemyAttack * enemyAttackRate) + enemyAttackDelta;
        int affectedEnemyDefence = (int)(enemyDefence * enemyDefenceRate) + enemyDefenceDelta;

        //Battle logic
        double braverDamageRate = 1.0;
        double enemyDamageRate = 1.0;
        int enemyDamageDelta = 0;
        //If enemy cannot break braver's defence
        if (affectedEnemyAttack - affectedBraverDefence <= 0)
        {
            return 0;
        }
        //Get braver damage each turn
        int braverTurnDamage = (int)((affectedBraverAttack - affectedEnemyDefence)*braverDamageRate);
        if (braverTurnDamage <= 0) //Can not break enemy's defence
        {
            return -1;
        }
        //Calculate turns to defeat enemy
        int turn = affectedEnemyHealth / braverTurnDamage;
        if (affectedEnemyHealth % braverTurnDamage == 0)
        {
            turn -= 1;
        }
        //Calculate enemy damage deal to braver
        int enemyDamage = (int)((affectedEnemyAttack - affectedBraverDefence) * enemyDamageRate) * turn + enemyDamageDelta - affectedBraverShield;
        if (enemyDamage < 0)
        {
            enemyDamage = 0;
        }
        return enemyDamage;
    }
}
