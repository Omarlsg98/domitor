using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static DiscreteCoordinate;
using static AttackSoundController;

public class AttackInstance : MonoBehaviour
{
    public DiscreteCoordinate actPosition;
    public CoolDown continuousDamageCoolDown;
    public CoolDown preDamageCoolDown;
    public CoolDown timeToDisappear;
    public bool destroyOnHit = true;
    public AttackSoundController soundController;

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
        soundController.setAudioSource(null);
        soundController.reproduceAttack();
        preDamageCoolDown.turnOnCooldown();
        timeToDisappear.turnOnCooldown();
    }

    private void checkHitWithDemons(){
        continuousDamageCoolDown.updateCoolDown();
        if (continuousDamageCoolDown.isReady()){
            GameObject[] demons = GameObject.FindGameObjectsWithTag("Demon");
            foreach (GameObject demon in demons)
            {
                Demon demonComponent = demon.GetComponent<Demon>();
                if (demonComponent.actPosition.Equals(this.actPosition)) {
                    continuousDamageCoolDown.turnOnCooldown();
                    demonComponent.applyHit(damage);
                    if (destroyOnHit){
                        Destroy(gameObject);
                    }
                }
            }
        }
    }
}
