using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static Grid; 
using static Demon;

public class Main : MonoBehaviour
{
    public Grid actualGrid;
    public int gridHorizontal = 6;
    public int gridVertical = 3;
    public float gridMinX = -3.0f;
    public float gridMinY = -1.0f;
    public float gridStep = 1.5f;
    public GameObject playerTilePrefab;
    public GameObject enemyTilePrefab;

    public Demon player {get; set;}
    public AI AIController {get; set;}

    // Start is called before the first frame update
    void Awake()
    {
        actualGrid = new Grid(gridHorizontal, gridVertical, gridMinX, gridMinY, gridStep, playerTilePrefab, enemyTilePrefab);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
