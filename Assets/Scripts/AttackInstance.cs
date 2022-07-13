using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static DiscreteCoordinate;

public class AttackInstance : MonoBehaviour
{
    public DiscreteCoordinate actPosition;
    public int damage;

    public int timeToDisappear = 10;
    public bool destroyOnHit = true;

    void Update()
    {
        if (actPosition == null) {
            return;
        }

        checkHitWithDemons();

        timeToDisappear -= 1;
        if (timeToDisappear <= 0){
            Destroy(gameObject);
        }
    }

    private void checkHitWithDemons(){
        GameObject[] demons = GameObject.FindGameObjectsWithTag("Demon");
        foreach (GameObject demon in demons)
        {
            DemonConfig demonConfig = demon.GetComponent<DemonConfig>();
            if (demonConfig.actPosition.Equals(this.actPosition)) {
                demonConfig.applyHit(damage);
                if (destroyOnHit){
                     Destroy(gameObject);
                }
            }
        }
    }
}
