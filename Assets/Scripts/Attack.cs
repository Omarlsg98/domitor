using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static Grid;
using static GridTile;
using static CoolDown;

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
    public CoolDown attackCoolDown;
    public CoolDown spawnCoolDown;
    public GameObject prefab;
    public AttackType attackType;
}

public abstract class Attack
{
    public bool isFinished = false;

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

    private int step;

    public RowAttack(bool isPlayer, DiscreteCoordinate actPosition, Grid grid, AttackConfig atcConfig){
        this.actPosition = actPosition;
        this.grid = grid;
        this.prefab = atcConfig.prefab;
        this.isPlayer = isPlayer;
        this.damage = atcConfig.damage;
        this.atcConfig = atcConfig;
        this.step = 0;
    }

    public override bool execute(){
        atcConfig.spawnCoolDown.updateCoolDown();
        if (atcConfig.spawnCoolDown.isReady()){
            List<DiscreteCoordinate> attacksCoords = rowGenerator(this.step);
            spawnAttacks(attacksCoords);
            atcConfig.spawnCoolDown.turnOnCooldown();
            this.step += 1;
        }
        return this.isFinished;
    }

    private List<DiscreteCoordinate> rowGenerator(int step){
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

    private void spawnAttacks(List<DiscreteCoordinate> coords){
        foreach(DiscreteCoordinate coord in coords){
            GameObject attack = ScriptableObject.Instantiate(prefab, grid.getTile(coord).getTransform());
            attack.GetComponent<AttackInstance>().actPosition = coord;
            attack.GetComponent<AttackInstance>().damage = damage;
        }
    }
}