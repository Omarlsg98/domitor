using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static Demon;
using static AIType;

public class AI : MonoBehaviour
{
    public GameObject demonPrefab;
    public int maxDemons;
    public float demonSpawnProbability = 0.01f;

    private Main mainController;
    private Grid grid;
    private List<Demon> demons;
    private List<AIType> AIs;

    void Start()
    {
        mainController = GameObject.FindWithTag("GameController").GetComponent<Main>();
        mainController.AIController = this;
        grid = mainController.actualGrid;
        demons = new List<Demon>();
        spawnDemon();       
    }

    // Update is called once per frame
    void Update()
    {   
        Movement();
        Attack();
    }

    void Movement()
    {
        //demon.updatePosition(horizontalAxis, verticalAxis);
    }

    void Attack()
    {
        // demon.attack();
    }

    private void spawnDemon(){
        DiscreteCoordinate newPosition = new DiscreteCoordinate(0, 3);
        GameObject demonGameObject = Instantiate(demonPrefab, grid.getTilePosition(newPosition), Quaternion.identity);
        Demon newDemon = demonGameObject.GetComponent<Demon>();
        newDemon.setup(false, grid, newPosition);
        demons.Add(newDemon);
    }
}
