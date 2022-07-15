using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static Grid;
using static DiscreteCoordinate;
using static Attack;
using static CoolDown;
using static DemonSoundController;

public class Demon : MonoBehaviour
{
    public int maxLife = 100;
    private int actualLife;

    public DemonSoundController soundController;
    public List<AttackConfig> availableAttacks;
    public DiscreteCoordinate actPosition;
    public CoolDown movementCoolDown;
    
    private AudioSource audioSource;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Grid grid;
    private List<Attack> attacksInProgress = new List<Attack>();
    private bool isPlayer;
    private float difficultyFactor;

    void Start(){
        GameObject capsuleChild = gameObject.transform.GetChild(0).gameObject;
        animator = capsuleChild.GetComponent<Animator>();
        spriteRenderer = capsuleChild.GetComponent<SpriteRenderer>(); 

        audioSource = gameObject.GetComponent<AudioSource>();
        soundController.setAudioSource(audioSource);
        if (isPlayer){
            capsuleChild.GetComponent<SpriteRenderer>().flipX = true;
        }
    }
    
    public void setup(bool isPlayer, Grid grid, DiscreteCoordinate actPosition, float difficultyFactor)
    {
        this.grid = grid;
        this.isPlayer = isPlayer;
        grid.getTile(actPosition).isEmpty = false;
        this.actPosition = actPosition;
        this.difficultyFactor = difficultyFactor;
        actualLife = maxLife;
    }

    void Update(){
        updateAllAvailableAttacks();
        executeAllAttacksInProgress();
    }

    public int updatePosition(int horizontalAxis, int verticalAxis)
    {
        if (movementCoolDown.isReady() && isAlive()){     
            DiscreteCoordinate newPosition = null;
            if (horizontalAxis == 1 | horizontalAxis == -1){
                newPosition =  new DiscreteCoordinate(actPosition.y, actPosition.x + horizontalAxis);
            }
            else if (verticalAxis == 1 | verticalAxis == -1){
                newPosition =  new DiscreteCoordinate(actPosition.y + verticalAxis, actPosition.x);
            }
            if (newPosition != null){
                if (grid.verifyPosition(newPosition, isPlayer)){
                    grid.getTile(actPosition).isEmpty = true;
                    grid.getTile(newPosition).isEmpty = false;
                    this.actPosition = newPosition;
                    gameObject.transform.position = grid.getTilePosition(newPosition);
                    movementCoolDown.turnOnCooldown();
                    soundController.reproduceMovement();
                    return 1;
                }else {
                    return 0;
                }
            }
        } else {
            movementCoolDown.updateCoolDown();
        }
        return -1;
    }

    public void applyHit(int damage){
        this.actualLife -= isPlayer? (int) (damage * difficultyFactor) : damage;
        float lifePercentage = (float)this.actualLife/this.maxLife;
        spriteRenderer.material.color = new Color(1.0f, lifePercentage, lifePercentage);
        soundController.reproduceDamage();
        animateDamage();
        if (!isAlive()){
            grid.getTile(actPosition).isEmpty = true;
            Destroy(gameObject);
        }
    }

    public bool isAlive(){
        return this.actualLife > 0;
    }
    
    public void attack(AttackButton selector){
        AttackConfig atcConfig = getAttackConfig(selector);
        if (atcConfig.attackCoolDown.isReady() && atcConfig.isUsable() && isAlive()){
            soundController.reproduceAttack();
            animateAttack();
            atcConfig.addUsage();
            Attack attack = Attack.getAttackInstance(isPlayer, actPosition, grid, atcConfig);
            attacksInProgress.Add(attack);
            atcConfig.attackCoolDown.turnOnCooldown();
        }
    }

    private AttackConfig getAttackConfig(AttackButton selector){
        foreach(AttackConfig atcConfig in availableAttacks){
            if (atcConfig.button == selector) {
                return atcConfig;
            }
        }
        return null;
    }
    
    private void updateAllAvailableAttacks(){
        foreach(AttackConfig atcConfig in availableAttacks){
            atcConfig.attackCoolDown.updateCoolDown();
        }
    }

    private void executeAllAttacksInProgress(){
        List<int> toDelete = new List<int>();
        for (int i = 0; i < attacksInProgress.Count; i++){
            Attack attack = attacksInProgress[i];
            if(attack.execute()){
                toDelete.Add(i);
            }
        }

        foreach(int index in toDelete){
            attacksInProgress.RemoveAt(index);
        }
    }

    private void animateAttack(){
        animator.SetTrigger("Attack01");
    }

    private void animateDamage(){
        animator.SetTrigger("Hurt");
    }
}