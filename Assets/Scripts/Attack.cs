using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static Grid;
using static GridTile;

public interface Attack
{
    bool execute();
}

public class RowAttack : Attack
{
    private int damage;
    private int [] tilesAffected;
    private int [] actPosition;

    public RowAttack(int damage, int [] actPosition){
        
    }

    public bool execute(){
        return true;
    }
}