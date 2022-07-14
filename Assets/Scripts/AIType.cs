using UnityEngine;
using static Demon;


public enum AIType
{
    Stalker,
    ColumnStalker,
    Turret,
    Bombardier
} 


public abstract class AIInstance{
    public Demon demon;
    public Demon player;

    protected int prevMovementResult;

    public static AIInstance getRandomInstance(Demon demon, Demon player){
        AIType randomAIType = (AIType)Random.Range(0, System.Enum.GetNames(typeof(AIType)).Length);
        switch (randomAIType)
        {
            case AIType.Stalker: 
            return new AIStalker(demon, player);

            case AIType.ColumnStalker: 
            return new AIColumnStalker(demon, player);

            case AIType.Turret: 
            return new AITurret(demon, player);         

            case AIType.Bombardier: 
            return new AIBombardier(demon, player);

            default: 
            return null;
        } 
    }

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
        if (attackBtn != AttackButton.None && player.isAlive()) { 
            demon.attack(attackBtn);
        }
    }

    protected int getVerticallDiff(){
        return player.actPosition.y - demon.actPosition.y;
    }

    protected int getHorizontalDiff(){
        return demon.actPosition.x - player.actPosition.x;
    }

    protected int getVerticalFollowCommand(){
        int verticalDiff = getVerticallDiff();
        if(verticalDiff == 0){
            return 0;
        }else if (verticalDiff > 0){
            return 1;
        }else {
            return -1;
        } 
    }

    protected int getHorizontalFollowCommand(){
        int horizontalDiff = getHorizontalDiff();
        if (horizontalDiff == 3){
            return 0;
        }else if (horizontalDiff > 3){
            return -1;
        }else{
            return 1;
        }
    }
}


public class AIStalker : AIInstance
{
    public AIStalker(Demon demon, Demon player) : base(demon, player) {}

    protected override (int horizontalAxis, int verticalAxis) getAxis(){
        (int horizontalAxis, int verticalAxis) = (0, 0);
        verticalAxis = getVerticalFollowCommand();

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


public class AIColumnStalker : AIInstance
{
    public AIColumnStalker(Demon demon, Demon player) : base(demon, player) {}

    protected override (int horizontalAxis, int verticalAxis) getAxis(){
        (int horizontalAxis, int verticalAxis) = (0, 0);
        horizontalAxis = getHorizontalFollowCommand();

        if(prevMovementResult == 0){
            verticalAxis = Random.Range(-1, 2);
        }
        
        return (horizontalAxis, verticalAxis);
    }

    protected override AttackButton getAttackButton(){
        if(getHorizontalDiff() == 3){
            return AttackButton.Attack3;
        }
        return AttackButton.None;
    }
}

public class AITurret : AIInstance
{
    public AITurret(Demon demon, Demon player) : base(demon, player) {}

    protected override (int horizontalAxis, int verticalAxis) getAxis(){
        (int horizontalAxis, int verticalAxis) = (0, 0);
        return (horizontalAxis, verticalAxis);
    }

    protected override AttackButton getAttackButton(){
        if(getVerticallDiff() == 0){
            return AttackButton.Attack1;
        }
        return AttackButton.None;
    }
}


public class AIBombardier : AIInstance
{
    public AIBombardier(Demon demon, Demon player) : base(demon, player) {}

    protected override (int horizontalAxis, int verticalAxis) getAxis(){
        (int horizontalAxis, int verticalAxis) = (0, 0);

        verticalAxis = getVerticalFollowCommand();
        horizontalAxis = getHorizontalFollowCommand();

        return (horizontalAxis, verticalAxis);
    }

    protected override AttackButton getAttackButton(){
        if(getVerticallDiff() == 0 && getHorizontalDiff() == 3){
            return AttackButton.Attack2;
        }
        return AttackButton.None;
    }
}