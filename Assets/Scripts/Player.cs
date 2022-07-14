using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static Demon;
using static Attack;

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

        DiscreteCoordinate newPosition = new DiscreteCoordinate(0, 0);
        GameObject demonGameObject = Instantiate(demonPrefab, grid.getTilePosition(newPosition), Quaternion.identity);
        demon = demonGameObject.GetComponent<Demon>();
        demon.setup(true, grid, newPosition);
        
        mainController.player = demon;
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
            demon.attack(AttackButton.Attack1);
        }
        if (Input.GetButtonDown("Attack2")) { 
            demon.attack(AttackButton.Attack2);
        }
        if (Input.GetButtonDown("Attack3")) { 
            demon.attack(AttackButton.Attack3);
        }
        if (Input.GetButtonDown("Attack4")) { 
            demon.attack(AttackButton.Attack4);
        }
    }
}
