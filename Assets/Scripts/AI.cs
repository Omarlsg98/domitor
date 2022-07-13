using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static Demon;

public class AI : MonoBehaviour
{
    public GameObject demonPrefab;
    private Main mainController;
    private Grid grid;
    
    private Demon demon;

    void Start()
    {
        mainController = GameObject.FindWithTag("GameController").GetComponent<Main>();
        grid = mainController.actualGrid;

        DiscreteCoordinate newPosition = new DiscreteCoordinate(0, 3);
        GameObject demonGameObject = Instantiate(demonPrefab, grid.getTilePosition(newPosition), Quaternion.identity);
        demon = demonGameObject.GetComponent<Demon>();
        demon.setup(false, grid, newPosition);
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
}
