using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static DiscreteCoordinate;

public class GridTile
{
    private GameObject tile;
    public bool isEmpty;
    public bool isPlayerTile;
    public DiscreteCoordinate postionInGrid;

    public GridTile(GameObject prefab, Vector3 position, bool isPlayerTile, DiscreteCoordinate postionInGrid){
        this.tile = ScriptableObject.Instantiate(prefab, position, Quaternion.identity);
        this.isPlayerTile = isPlayerTile;
        this.postionInGrid = postionInGrid;
        this.isEmpty = true;
    }

    public Vector3 getCoordinates() {
        return tile.transform.position;
    }

    public Transform getTransform(){
        return tile.transform;
    } 
}