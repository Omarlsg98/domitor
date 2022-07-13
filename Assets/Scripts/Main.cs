using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Grid; 

public class Main : MonoBehaviour
{
    private Grid actualGrid;
    public int gridHorizontal = 6;
    public int gridVertical = 3;
    public GameObject playerTilePrefab;
    public GameObject enemyTilePrefab;

    // Start is called before the first frame update
    void Start()
    {
        actualGrid = new Grid(gridHorizontal, gridVertical, -3, -1, 1, playerTilePrefab, enemyTilePrefab);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public Grid getActualGrid(){
        return actualGrid;
    }
}
