using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MyAudioClip{
    public AudioClip audio;
    public int secondsToWait;
}

public abstract class SoundController
{
    protected AudioSource audioSource;
    private Main mainController;

    public void setAudioSource(AudioSource audioSource){
        this.audioSource = audioSource;
        mainController = GameObject.FindWithTag("GameController").GetComponent<Main>();
    }
    
    protected void reproduceSound(MyAudioClip clip){
        mainController.StartCoroutine(_reproduceSound(clip));
    }

    private IEnumerator _reproduceSound(MyAudioClip clip){
        yield return new WaitForSeconds(clip.secondsToWait);
        audioSource.PlayOneShot(clip.audio);
    }
}

[System.Serializable]
public class DemonSoundController : SoundController
{
    public MyAudioClip damageAudio;
    public MyAudioClip movementAudio;
    public MyAudioClip attackAudio;

   
    public void reproduceAttack(){
        reproduceSound(attackAudio);
    }

    public void reproduceAttack(MyAudioClip attackAudio){
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
}