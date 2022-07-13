using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static Attack;

public class DemonConfig : MonoBehaviour
{
    public int maxLife = 100;
    public int actualLife = 100;
    public GameObject attackPrefab;
    public AttackType attackType;
}