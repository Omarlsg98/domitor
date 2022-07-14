using UnityEngine;
using static Demon;

public enum AIType
{
    Persecutor,
    Turret,
    Bombard
} 

public abstract class AIInstance{
    public Demon demon;
    public Demon player;

    protected int prevMovementResult;

    public AIInstance(Demon demon, Demon player){
        this.demon = demon;
        this.player = player;
        prevMovementResult = -1;
    }

    protected abstract (int horizontalAxis, int verticalAxis) getAxis();
    protected abstract AttackButton getAttackButton();

    public void Movement()
    {
        (int horizontalAxis, int verticalAxis)  = getAxis();
        prevMovementResult = demon.updatePosition(horizontalAxis, verticalAxis);
    }

    public void Attack()
    {
        AttackButton attackBtn = getAttackButton();
        if (attackBtn != AttackButton.None) { 
            demon.attack(attackBtn);
        }
    }

    protected int getVerticallDiff(){
        return player.actPosition.y - demon.actPosition.y;
    }
}

public class AIStalker : AIInstance
{
    public AIStalker(Demon demon, Demon player) : base(demon, player) {}

    protected override (int horizontalAxis, int verticalAxis) getAxis(){
        (int horizontalAxis, int verticalAxis) = (0, 0);
        int verticalDiff = getVerticallDiff();
        if(verticalDiff == 0){
            verticalAxis = 0;
        }else if (verticalDiff > 0){
            verticalAxis = 1;
        }else {
            verticalAxis = -1;
        }

        if(prevMovementResult == 0){
            horizontalAxis = Random.Range(-1, 2);
        }
        
        return (horizontalAxis, verticalAxis);
    }

    protected override AttackButton getAttackButton(){
        if(getVerticallDiff() == 0){
            return AttackButton.Attack1;
        }
        return AttackButton.None;
    }
}