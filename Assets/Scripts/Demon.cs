using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static Grid;
using static GridTile;

public class Demon : ScriptableObject
{
    public int maxLife = 100;
    public int actualLife = 100;

    private GameObject actDemon;

    private int[] actPosition;
    private Grid grid;
    private int movementRefreshTimer;
    private int timeBetweenMovement;

    private bool isPlayer;

    public Demon(GameObject prefab, bool isPlayer, Grid grid, int timeBetweenMovement)
    {
        this.grid = grid;
        movementRefreshTimer = 0;
        this.timeBetweenMovement = timeBetweenMovement;

        this.isPlayer = isPlayer;
        if (isPlayer){
            actPosition = new int[] {0, 0}; // y, x
        } else {
            actPosition = new int[] {2, 5};
        }

        actDemon = Instantiate(prefab, grid.getTilePosition(actPosition), Quaternion.identity);
    }


    public void updatePosition(int horizontalAxis, int verticalAxis)
    {
        if (movementRefreshTimer > 0){
            movementRefreshTimer -= 1;
        } else {
            int [] newPosition = null;
            if (horizontalAxis == 1 | horizontalAxis == -1){
                newPosition =  new int[] {actPosition[0], actPosition[1] + horizontalAxis};
            }
            else if (verticalAxis == 1 | verticalAxis == -1){
                newPosition =  new int[] {actPosition[0] + verticalAxis, actPosition[1]};
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
        
    }
}