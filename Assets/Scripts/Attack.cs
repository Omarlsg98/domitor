using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static Grid;
using static GridTile;

public enum AttackType
{
    RowAttack 
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
    public int timeBetweenAttacks = 120;
    public GameObject prefab;
    public AttackType attackType;
    
    public int attackRefreshTimer;
}

public abstract class Attack
{
    public abstract bool execute();
}

public class RowAttack : Attack
{
    private DiscreteCoordinate actPosition;
    private Grid grid;
    private GameObject prefab;
    private bool isPlayer;
    private int damage;
    private AttackConfig atcConfig;

    public RowAttack(bool isPlayer, DiscreteCoordinate actPosition, Grid grid, AttackConfig atcConfig){
        this.actPosition = actPosition;
        this.grid = grid;
        this.prefab = atcConfig.prefab;
        this.isPlayer = isPlayer;
        this.damage = atcConfig.damage;
        this.atcConfig = atcConfig;
    }

    public override bool execute(){
        //TODO: use yield? and general update to execute the attack
        int horizontalGridSize = grid.getHorizontalSize(actPosition.y);
        int enemyStartX = grid.getEnemyStartX(actPosition.y);
        for (int i = enemyStartX; i < horizontalGridSize; i++) 
        {
            int new_x = isPlayer? i : (enemyStartX - i);
            DiscreteCoordinate coord = new DiscreteCoordinate(actPosition.y, new_x);
            GameObject attack = ScriptableObject.Instantiate(prefab, grid.getTile(coord).getTransform());
            attack.GetComponent<AttackInstance>().actPosition = coord;
            attack.GetComponent<AttackInstance>().damage = damage;
        }
        return true;
    }
}