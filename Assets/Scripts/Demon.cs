using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static Grid;
using static DiscreteCoordinate;
using static Attack;
using static CoolDown;
public class Demon : MonoBehaviour
{
    public int maxLife = 100;
    public int actualLife = 100;

    public List<AttackConfig> availableAttacks;
    private List<Attack> attacksInProgress = new List<Attack>();

    public DiscreteCoordinate actPosition;
    private Grid grid;
    public CoolDown movementCoolDown;

    private bool isPlayer;

    public void setup(bool isPlayer, Grid grid, DiscreteCoordinate actPosition)
    {
        this.grid = grid;
        this.isPlayer = isPlayer;
        this.actPosition = actPosition; 
    }

    void Update(){
        updateAllAvailableAttacks();
        executeAllAttacksInProgress();
    }

    public void updatePosition(int horizontalAxis, int verticalAxis)
    {
        if (movementCoolDown.isReady()){     
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
                    movementCoolDown.turnOnCooldown();
                }
            }
        } else {
            movementCoolDown.updateCoolDown();
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
        if (atcConfig.attackCoolDown.isReady()){
            Attack attack = Attack.getAttackInstance(isPlayer, actPosition, grid, atcConfig);
            attacksInProgress.Add(attack);
            atcConfig.attackCoolDown.turnOnCooldown();
        }
    }

    private AttackConfig getAttackConfig(AttackButton selector){
        foreach(AttackConfig atcConfig in availableAttacks){
            if (atcConfig.button == selector) {
                return atcConfig;
            }
        }
        return null;
    }
    
    private void updateAllAvailableAttacks(){
        foreach(AttackConfig atcConfig in availableAttacks){
            atcConfig.attackCoolDown.updateCoolDown();
        }
    }

    private void executeAllAttacksInProgress(){
        List<int> toDelete = new List<int>();
        for (int i = 0; i < attacksInProgress.Count; i++){
            Attack attack = attacksInProgress[i];
            if(attack.execute()){
                toDelete.Add(i);
            }
        }

        foreach(int index in toDelete){
            attacksInProgress.RemoveAt(index);
        }
    }
}