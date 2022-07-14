using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static GridTile;
using static DiscreteCoordinate;

public class Grid
{   
    private List<List<GridTile>> grid;

    public Grid(int horizontal_size, int vertical_size, float min_x, float min_y, 
                float step, GameObject playerTilePrefab, GameObject enemyTilePrefab)
    {
        grid = new List<List<GridTile>>();

        for (int j = 0; j < vertical_size; j++) 
        {
            List<GridTile> row_grid = new List<GridTile>();
            float current_y = min_y + (step * j);
            
            for (int i = 0; i < horizontal_size; i++) 
            {
                float current_x = min_x + (step * i);
                Vector3 tile_position = new Vector3(current_x, current_y, 0.0f);
                bool isPlayerTile =  i < horizontal_size/2;
                
                GameObject prefab;
                if (isPlayerTile){
                    prefab = playerTilePrefab;
                } else {
                    prefab = enemyTilePrefab;
                }
                DiscreteCoordinate positionInGrid = new DiscreteCoordinate(j, i);
                GridTile tile = new GridTile(prefab, tile_position, isPlayerTile, positionInGrid);
                row_grid.Add(tile);
            }

            grid.Add(row_grid);
        }
    }

    public bool verifyIsInRange(DiscreteCoordinate position){
        int y = position.y;
        int x = position.x;
        if (y < 0 | x < 0 | y >= grid.Count){
            return false;
        }
        if (x >= grid[y].Count){
            return false;
        }
        return true;
    }

    public bool verifyPosition(DiscreteCoordinate position, bool isPlayer){
        int y = position.y;
        int x = position.x;
        if (!verifyIsInRange(position)){
            return false;
        }
        GridTile actTile = getTile(position);
        return actTile.isPlayerTile == isPlayer & actTile.isEmpty;
    }
    
    public Vector3 getTilePosition(DiscreteCoordinate position){
        GridTile actTile = getTile(position);
        return actTile.getCoordinates();
    }

    public GridTile getTile(DiscreteCoordinate position){
        if (!verifyIsInRange(position)){
            throw new System.ArgumentException("Grid Position requested is not in range.");
        }
        return grid[position.y][position.x];
    }

    public int getHorizontalSize(int y){
        return grid[y].Count;
    }

    public int getVerticalSize(){
        return grid.Count;
    }

    public int getEnemyStartX(){
        return ((int)grid[0].Count/2);
    }

    public DiscreteCoordinate generateRandomCoordinate(bool inPlayerSide, bool mustBeEmptyTile){
        DiscreteCoordinate newPosition = new DiscreteCoordinate(-1, -1);
        
        int sanityCount = 0;
        while (!verifyPosition(newPosition, inPlayerSide)){
            newPosition = MyRandom.randomCoordinate(0, getEnemyStartX(), 0, getVerticalSize());
            if (!inPlayerSide){
                newPosition.x += getEnemyStartX();
            }
            if (!mustBeEmptyTile){
                return newPosition;
            }
            
            sanityCount += 1;
            if (sanityCount > 10000){
                return null;
            }
        }

        return newPosition;
        
    }
}