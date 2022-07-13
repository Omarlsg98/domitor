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
        demon = new Demon(demonPrefab, false, grid);
    }

    // Update is called once per frame
    void Update()
    {   
        demon.generalUpdate();
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
