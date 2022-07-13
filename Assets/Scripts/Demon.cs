using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static Grid;
using static DiscreteCoordinate;
using static DemonConfig;
using static Attack;

public class Demon
{
    private GameObject actDemon;

    private DiscreteCoordinate actPosition;
    private Grid grid;
    private int movementRefreshTimer;
    private int timeBetweenMovement;

    private int timeBetweenAttacks;
    private int attackRefreshTimer;

    private bool isPlayer;
    private DemonConfig config;

    public Demon(GameObject prefab, bool isPlayer, Grid grid)
    {
        this.grid = grid;
        movementRefreshTimer = 0;

        this.isPlayer = isPlayer;
        if (isPlayer){
            setActPosition(new DiscreteCoordinate(0, 0)); // y, x
        } else {
            setActPosition(new DiscreteCoordinate(0, 3));
        }
        actDemon = ScriptableObject.Instantiate(prefab, grid.getTilePosition(actPosition), Quaternion.identity);
        this.config = this.actDemon.GetComponent<DemonConfig>();
        this.config.actPosition = actPosition;
        this.timeBetweenMovement = this.config.timeBetweenMovement;
        this.timeBetweenAttacks = this.config.timeBetweenAttacks;
    }

    public void generalUpdate(){

    }

    public void updatePosition(int horizontalAxis, int verticalAxis)
    {
        if (movementRefreshTimer > 0){
            movementRefreshTimer -= 1;
        } else {
            DiscreteCoordinate newPosition = null;
            if (horizontalAxis == 1 | horizontalAxis == -1){
                newPosition =  new DiscreteCoordinate(actPosition.y, actPosition.x + horizontalAxis);
            }
            else if (verticalAxis == 1 | verticalAxis == -1){
                newPosition =  new DiscreteCoordinate(actPosition.y + verticalAxis, actPosition.x);
            }
            if (newPosition != null){
                if (grid.verifyPosition(newPosition, true)){
                    setActPosition(newPosition);
                    actDemon.transform.position = grid.getTilePosition(newPosition);
                    movementRefreshTimer = timeBetweenMovement;
                    
                }
            }
        }
    }

    public void attack(){
        Attack attack;
        switch (this.config.attackType)
        {
            case AttackType.RowAttack: 
            attack = new RowAttack(isPlayer, actPosition, grid, this.config.attackPrefab, this.config.damage);
            break;

            default: 
            return;
        }
        //TODO: time to next attack
        attack.execute();
    }

    private void setActPosition(DiscreteCoordinate newPosition){
        this.actPosition = newPosition;
        if (this.config != null){
            this.config.actPosition = newPosition;
        }
    }
}