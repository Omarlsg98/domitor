using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static DiscreteCoordinate;

public class AttackInstance : MonoBehaviour
{
    public DiscreteCoordinate actPosition;
    public CoolDown damageCoolDown;
    public CoolDown preDamageCoolDown;
    public CoolDown timeToDisappear;
    public bool destroyOnHit = true;

    private int damage;

    void Update()
    {
        if (actPosition == null) {
            return;
        }

        preDamageCoolDown.updateCoolDown();
        if (preDamageCoolDown.isReady()){
            checkHitWithDemons();

            timeToDisappear.updateCoolDown();
            if (timeToDisappear.isReady()){
                Destroy(gameObject);
            }
        }
    }

    public void setDamage(int damage){
        this.damage = damage;
        preDamageCoolDown.turnOnCooldown();
        timeToDisappear.turnOnCooldown();
    }

    private void checkHitWithDemons(){
        damageCoolDown.updateCoolDown();
        if (damageCoolDown.isReady()){
            GameObject[] demons = GameObject.FindGameObjectsWithTag("Demon");
            foreach (GameObject demon in demons)
            {
                Demon demonComponent = demon.GetComponent<Demon>();
                if (demonComponent.actPosition.Equals(this.actPosition)) {
                    damageCoolDown.turnOnCooldown();
                    demonComponent.applyHit(damage);
                    if (destroyOnHit){
                        Destroy(gameObject);
                    }
                }
            }
        }
    }
}
