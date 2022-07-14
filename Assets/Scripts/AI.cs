using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static Demon;
using static AIType;
using static MyRandom;

public class AI : MonoBehaviour
{
    public GameObject demonPrefab;
    public int maxDemons = 3;
    public float demonSpawnProbability = 0.00001f;

    private Main mainController;
    private Grid grid;
    public List<AIInstance> demons;

    void Start()
    {
        mainController = GameObject.FindWithTag("GameController").GetComponent<Main>();
        mainController.AIController = this;
        grid = mainController.actualGrid;
        demons = new List<AIInstance>();
        spawnDemon();       
    }

    // Update is called once per frame
    void Update()
    {   
        if(MyRandom.randomBool(demonSpawnProbability)){
            spawnDemon();
        }
        updateDemonList();
    }

    private void spawnDemon(){
        if (getNumberDemons() < maxDemons){
            DiscreteCoordinate newPosition = grid.generateRandomCoordinate(false, true);
            GameObject demonGameObject = Instantiate(demonPrefab, grid.getTilePosition(newPosition), Quaternion.identity);
            Demon newDemon = demonGameObject.GetComponent<Demon>();
            newDemon.setup(false, grid, newPosition);

            AIPersecutor fullNewDemon = new AIPersecutor(newDemon);
            demons.Add(fullNewDemon);
        }
    }

    private int getNumberDemons(){
        return demons.Count;
    }

    private void updateDemonList(){
        List<int> toDelete = new List<int>();
        for (int i = 0; i < demons.Count; i++){
            AIInstance instance = demons[i];
            if(!instance.demon.isAlive()){
                toDelete.Add(i);
            }
        }
        foreach(int index in toDelete){
            demons.RemoveAt(index);
        }
    }
}
