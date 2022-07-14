using static Demon;

public enum AIType
{
    Persecutor,
    Turret,
    Bombard
} 

public abstract class AIInstance{
    public Demon demon;

    public AIInstance(Demon demon){
        this.demon = demon;
    }
}

public class AIPersecutor : AIInstance
{
    public AIPersecutor(Demon demon) : base(demon)    
    {
    }
}