using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static Grid;
using static GridTile;
using static CoolDown;

public enum AttackType
{
    RowAttack,
    RangeAttack
}

public enum AttackButton
{
    Attack1,
    Attack2,
    Attack3,
    Attack4
}

[System.Serializable]
public class AttackConfig {
    public AttackButton button;

    public int damage = 10;
    public CoolDown castCoolDown;
    public CoolDown attackCoolDown;
    public CoolDown spawnCoolDown;
    public GameObject prefab;
    public AttackType attackType;

    public AudioClip attackMovementSound;

    public int range;
}

public abstract class Attack
{
    protected bool isFinished = false;
    public abstract bool execute();

    public static Attack getAttackInstance(bool isPlayer, DiscreteCoordinate actPosition, Grid grid, AttackConfig atcConfig){
        switch (atcConfig.attackType)
        {
            case AttackType.RowAttack: 
            return new RowAttack(isPlayer, actPosition, grid, atcConfig);

            case AttackType.RangeAttack: 
            return new RangeAttack(isPlayer, actPosition, grid, atcConfig);

            default: 
            return null;
        }
    }
}

public abstract class SimpleAttack : Attack
{
    protected DiscreteCoordinate actPosition;
    protected Grid grid;
    protected GameObject prefab;
    protected bool isPlayer;
    protected int damage;
    protected AttackConfig atcConfig;

    protected int step;

    public SimpleAttack(bool isPlayer, DiscreteCoordinate actPosition, Grid grid, AttackConfig atcConfig){
        this.actPosition = actPosition;
        this.grid = grid;
        this.prefab = atcConfig.prefab;
        this.isPlayer = isPlayer;
        this.damage = atcConfig.damage;
        this.atcConfig = atcConfig;
        this.step = 0;
        atcConfig.castCoolDown.turnOnCooldown();
    }

    public override bool execute(){
        atcConfig.castCoolDown.updateCoolDown();
        if (atcConfig.castCoolDown.isReady()){
            atcConfig.spawnCoolDown.updateCoolDown();
            if (atcConfig.spawnCoolDown.isReady()){
                List<DiscreteCoordinate> attacksCoords = attackCoordinatesGenerator(this.step);
                spawnAttacks(attacksCoords);
                atcConfig.spawnCoolDown.turnOnCooldown();
                this.step += 1;
            }
        }
        return this.isFinished;
    }

    private void spawnAttacks(List<DiscreteCoordinate> coords){
        foreach(DiscreteCoordinate coord in coords){
            GameObject attack = ScriptableObject.Instantiate(prefab, grid.getTile(coord).getTransform());
            attack.GetComponent<AttackInstance>().actPosition = coord;
            attack.GetComponent<AttackInstance>().setDamage(damage);
        }
    }

    protected abstract List<DiscreteCoordinate> attackCoordinatesGenerator(int step);
}

public class RowAttack : SimpleAttack
{
    public RowAttack(bool isPlayer, DiscreteCoordinate actPosition, Grid grid, AttackConfig atcConfig) :
                 base(isPlayer, actPosition, grid, atcConfig)    
    {
    }

    protected override List<DiscreteCoordinate> attackCoordinatesGenerator(int step){
        List<DiscreteCoordinate> result = new List<DiscreteCoordinate>();
        int horizontalGridSize = grid.getHorizontalSize(actPosition.y);
        int enemyStartX = grid.getEnemyStartX(actPosition.y);
        int i = enemyStartX + step;
        if (i < horizontalGridSize){
            int new_x = isPlayer? i : (enemyStartX - i);
            DiscreteCoordinate coord = new DiscreteCoordinate(actPosition.y, new_x);
            result.Add(coord);
        } else {
            this.isFinished = true; 
        }
        return result;
    }
}

public class RangeAttack : SimpleAttack
{
    public RangeAttack(bool isPlayer, DiscreteCoordinate actPosition, Grid grid, AttackConfig atcConfig) :
                 base(isPlayer, actPosition, grid, atcConfig)    
    {
    }

    protected override List<DiscreteCoordinate> attackCoordinatesGenerator(int step){
        List<DiscreteCoordinate> result = new List<DiscreteCoordinate>();
        int horizontalGridSize = grid.getHorizontalSize(actPosition.y);
        int enemyStartX = grid.getEnemyStartX(actPosition.y);
    
        int new_x = isPlayer? actPosition.x + atcConfig.range : actPosition.x - atcConfig.range;
        DiscreteCoordinate coord = new DiscreteCoordinate(actPosition.y, new_x);
        result.Add(coord);
        this.isFinished = true; 
    
        return result;
    }
}