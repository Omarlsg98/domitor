using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static DiscreteCoordinate;

public class AttackInstance : MonoBehaviour
{
    public DiscreteCoordinate actPosition;

    public int timeToDisappear = 10;
    public bool destroyOnHit = true;

    private int damage;
    
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

    public void setDamage(int damage){
        this.damage = damage;
    }

    private void checkHitWithDemons(){
        GameObject[] demons = GameObject.FindGameObjectsWithTag("Demon");
        foreach (GameObject demon in demons)
        {
            Demon demonComponent = demon.GetComponent<Demon>();
            if (demonComponent.actPosition.Equals(this.actPosition)) {
                demonComponent.applyHit(damage);
                if (destroyOnHit){
                     Destroy(gameObject);
                }
            }
        }
    }
}
