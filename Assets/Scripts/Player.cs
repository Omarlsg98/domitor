using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static Demon;

public class Player : MonoBehaviour
{
    public GameObject demonPrefab;
    private Main mainController;
    private Grid grid;
    private Demon demon;

    void Start()
    {
        mainController = GameObject.FindWithTag("GameController").GetComponent<Main>();
        grid = mainController.actualGrid;
        demon = new Demon(demonPrefab, true, grid);
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
        int horizontalAxis = (int)Input.GetAxis("Horizontal");
        int verticalAxis = (int)Input.GetAxis("Vertical");
        demon.updatePosition(horizontalAxis, verticalAxis);
    }

    void Attack()
    {
        if (Input.GetButtonDown("Attack1")) { 
            demon.attack();
        } 
    }
}
