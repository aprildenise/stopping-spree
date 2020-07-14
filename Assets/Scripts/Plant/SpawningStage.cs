using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpawningStage : PlantStage
{


    public float spawnRadius;
    public float timeToFruit;
    public float timeToPropagate;
    public int mutateChance;
    private Timer spawnFruitTimer;
    private Timer propagateTimer;

    protected void Start()
    {
        spawnFruitTimer = gameObject.AddComponent<Timer>();
        propagateTimer = gameObject.AddComponent<Timer>();
        spawnFruitTimer.SetTimer(timeToFruit);
        propagateTimer.SetTimer(timeToPropagate);
        spawnFruitTimer.StartTimer();
        propagateTimer.StartTimer();
        OnStart();
    }

    protected virtual void OnStart()
    {
        return;
    }

    protected void LateUpdate()
    {

        OnLateUpdate();

        if (spawnFruitTimer.GetStatus() == Timer.Status.FINISHED)
        {
            SpawnFruit();
        }

        if (propagateTimer.GetStatus() == Timer.Status.FINISHED)
        {
            Propogate();
        }

    }

    protected virtual void OnLateUpdate()
    {
        return;
    }

    private void SpawnFruit()
    {
        Debug.Log(stageController.parentPlant.gameObject.name + ":Spawn fruit");
        spawnFruitTimer.ResetTimer();
        spawnFruitTimer.StartTimer();

        
        Vector3 pos = new Vector3(
            Random.Range(transform.position.x - spawnRadius, transform.position.x + spawnRadius),
            0f, Random.Range(transform.position.z - spawnRadius, transform.position.z + spawnRadius));


        PrefabManager.instance.InitPrefab("Test Fruit", pos);
    }

    private void Propogate()
    {
        int random = Random.Range(1, 3 + 1);
        //if (random < mutateChance)
        //{
        //    Debug.Log(stageController.parentPlant.gameObject.name + ": Propogating new type");
        //}
        //else
        //{
        //    Debug.Log(stageController.parentPlant.gameObject.name + ": Propogating same type");
        //}
        propagateTimer.ResetTimer();
        propagateTimer.StartTimer();

        Vector3 pos = new Vector3(
            Random.Range(transform.position.x - spawnRadius, transform.position.x + spawnRadius),
            0f, Random.Range(transform.position.z - spawnRadius, transform.position.z + spawnRadius));
        GameObject spawn = PrefabManager.instance.InitPrefab(random, pos);

        Debug.Log(stageController.parentPlant.gameObject.name + ": Propogating - " + spawn.name);
    }
}
