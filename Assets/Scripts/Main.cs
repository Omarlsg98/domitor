using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using static Grid; 
using static Demon;

public class Main : MonoBehaviour
{
    public Grid actualGrid;
    public float difficultyFactor = 0.5f;
    public int gridHorizontal = 6;
    public float gridMinX = -3.0f;
    public float gridMinY = -1.0f;
    public float gridHorizontalStep = 1.5f;
    public float gridVerticalStep = 1.5f;
    public List<GameObject> playerTilesPrefab;
    public List<GameObject> enemyTilesPrefab;

    public Demon player {get; set;}
    public AI AIController {get; set;}
    public GameObject textKilled;

    private int demonsKilled = 0;

    // Start is called before the first frame update
    void Awake()
    {
        actualGrid = new Grid(gridHorizontal, gridMinX, gridMinY, gridHorizontalStep, gridVerticalStep, playerTilesPrefab, enemyTilesPrefab);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void increaseDemonsKilled(){
        demonsKilled += 1;
        textKilled.GetComponent<TextMeshProUGUI>().text = "Killed: " +demonsKilled;
    }
}
