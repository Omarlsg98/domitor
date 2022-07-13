using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static GridTile;
using static DiscreteCoordinate;

public class Grid
{   
    private List<List<GridTile>> grid;

    public Grid(int horizontal_size, int vertical_size, float min_x, float min_y, 
                int step, GameObject playerTilePrefab, GameObject enemyTilePrefab)
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

                GridTile tile = new GridTile(prefab, tile_position, isPlayerTile);
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

    private GridTile getTile(DiscreteCoordinate position){
        if (!verifyIsInRange(position)){
            throw new ArgumentException("Grid Position requested is not in range.");
        }
        return grid[position.y][position.x];
    }

}