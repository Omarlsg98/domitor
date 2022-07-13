using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTile
{
    private GameObject tile;
    public bool isEmpty;
    public bool isPlayerTile;

    public GridTile(GameObject prefab, Vector3 position, bool isPlayerTile){
        this.tile = ScriptableObject.Instantiate(prefab, position, Quaternion.identity);
        this.isPlayerTile = isPlayerTile;
        this.isEmpty = true;
    }

    public Vector3 getCoordinates() {
        return tile.transform.position;
    }
    public Transform getTransform(){
        return tile.transform;
    }
}