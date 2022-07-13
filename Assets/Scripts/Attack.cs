using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static Grid;
using static GridTile;

public enum AttackType
{
    RowAttack 
}

public abstract class Attack : MonoBehaviour
{
    public abstract bool execute();
}

public class RowAttack : Attack
{
    private DiscreteCoordinate actPosition;
    private Grid grid;
    private GameObject prefab;
    private bool isPlayer;

    public RowAttack(bool isPlayer, DiscreteCoordinate actPosition, Grid grid, GameObject prefab){
        this.actPosition = actPosition;
        this.grid = grid;
        this.prefab = prefab;
        this.isPlayer = isPlayer;
    }

    public override bool execute(){
        int horizontalGridSize = grid.getHorizontalSize(actPosition.y);
        int enemyStartX = grid.getEnemyStartX(actPosition.y);
        for (int i = enemyStartX; i < horizontalGridSize; i++) 
        {
            int new_x = isPlayer? i : (enemyStartX - i);
            DiscreteCoordinate coord = new DiscreteCoordinate(actPosition.y, new_x);
            GameObject attack = ScriptableObject.Instantiate(prefab, grid.getTile(coord).getTransform());
        }
        return true;
    }
}