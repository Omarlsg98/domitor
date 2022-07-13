using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static Grid;
using static DiscreteCoordinate;
using static Attack;

public class Demon : MonoBehaviour
{
    public int maxLife = 100;
    public int actualLife = 100;

    public List<AttackConfig> availableAttacks;

    public DiscreteCoordinate actPosition;
    private Grid grid;
    private int movementRefreshTimer;
    public int timeBetweenMovement = 90;

    private bool isPlayer;

    public void setup(bool isPlayer, Grid grid, DiscreteCoordinate actPosition)
    {
        this.grid = grid;
        movementRefreshTimer = 0;

        this.isPlayer = isPlayer;
        this.actPosition = actPosition; 
    }

    void Update(){

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
                    this.actPosition = newPosition;
                    gameObject.transform.position = grid.getTilePosition(newPosition);
                    movementRefreshTimer = timeBetweenMovement;
                    
                }
            }
        }
    }

    public void applyHit(int damage){
        this.actualLife -= damage;
        if (this.actualLife <= 0){
            Destroy(gameObject);
        }
    }
    
    public void attack(AttackButton selector){
        AttackConfig atcConfig = getAttackConfig(selector);

        Attack attack;
        switch (atcConfig.attackType)
        {
            case AttackType.RowAttack: 
            attack = new RowAttack(isPlayer, actPosition, grid, atcConfig);
            break;

            default: 
            return;
        }
        //TODO: time to next attack
        attack.execute();
    }

    private AttackConfig getAttackConfig(AttackButton selector){
        foreach(AttackConfig atcConfig in availableAttacks){
            if (atcConfig.button == selector) {
                return atcConfig;
            }
        }
        return null;
    }
    
}