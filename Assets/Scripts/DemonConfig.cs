using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static Attack;

public class DemonConfig : MonoBehaviour
{
    public int maxLife = 100;
    public int actualLife = 100;

    public int damage = 10;
    public int timeBetweenAttacks = 120;
    public GameObject attackPrefab;
    public AttackType attackType;

    public int timeBetweenMovement = 90;

    public DiscreteCoordinate actPosition;

    public void applyHit(int damage){
        this.actualLife -= damage;
        if (this.actualLife <= 0){
            Destroy(gameObject);
        }
    }
}