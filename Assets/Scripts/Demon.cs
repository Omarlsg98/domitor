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

    private bool isPlayer;
    private DemonConfig config;

    public Demon(GameObject prefab, bool isPlayer, Grid grid, int timeBetweenMovement)
    {
        this.grid = grid;
        movementRefreshTimer = 0;
        this.timeBetweenMovement = timeBetweenMovement;

        this.isPlayer = isPlayer;
        if (isPlayer){
            actPosition = new DiscreteCoordinate(0, 0); // y, x
        } else {
            actPosition = new DiscreteCoordinate(2, 5);
        }

        actDemon = ScriptableObject.Instantiate(prefab, grid.getTilePosition(actPosition), Quaternion.identity);
        this.config = this.actDemon.GetComponent<DemonConfig>();
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
                    actPosition = newPosition;
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
            attack = new RowAttack(isPlayer, actPosition, grid, this.config.attackPrefab);
            break;

            default: 
            return;
        }
        attack.execute();
    }
}