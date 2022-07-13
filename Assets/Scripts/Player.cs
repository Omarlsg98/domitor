using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static Demon;

public class Player : MonoBehaviour
{
    public GameObject demonPrefab;
    private Main mainController;
    private Grid grid;
    
    public int timeBetweenMovement = 90;
    public Demon demon;

    void Start()
    {
        mainController = GameObject.FindWithTag("GameController").GetComponent<Main>();
        grid = mainController.getActualGrid();
        demon = new Demon(demonPrefab, true, grid, timeBetweenMovement);
    }

    // Update is called once per frame
    void Update()
    {
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
