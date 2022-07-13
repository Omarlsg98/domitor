using UnityEngine;

[System.Serializable]
public class DemonSoundController
{
    public AudioSource audioSource;
    public AudioClip damageAudio;
    public AudioClip movementAudio;
    public AudioClip attackAudio;

    public void reproduceAttack(){
        reproduceSound(attackAudio);
    }

    public void reproduceAttack(AudioClip attackAudio){
        if (attackAudio == null){
            reproduceSound(this.attackAudio);
        }else {
            reproduceSound(attackAudio);
        }
    }

    public void reproduceMovement(){
        reproduceSound(movementAudio);
    }

    public void reproduceDamage(){
        reproduceSound(damageAudio);
    }

    private void reproduceSound(AudioClip audio){
        audioSource.PlayOneShot(audio);
    }
}