[System.Serializable]
public class CoolDown
{
    public int timeToWait;
    private int actualTimeLeft = 0;

    public void updateCoolDown(){
        if (!isReady()){
            actualTimeLeft -= 1;
        }
    }

    public void turnOnCooldown(){
        actualTimeLeft = timeToWait;
    }

    public bool isReady(){
        return actualTimeLeft <= 0;
    }
}