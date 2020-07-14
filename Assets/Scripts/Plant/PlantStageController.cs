using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantStageController : MonoBehaviour
{


    public Plant parentPlant;
    public List<PlantStage> stages;

    private int currentStageIndex = -1;
    public PlantStage currentStage;


    public float timeToNextStage;
    private Timer toNextStageTimer;


    private void Awake()
    {

        parentPlant = GetComponent<Plant>();

        foreach(Transform child in transform)
        {
            if (!child.gameObject.CompareTag("Stage")) continue;
            PlantStage stage = child.GetComponent<PlantStage>();
            stage.stageController = this;
            stages.Add(stage);
            stage.gameObject.SetActive(false);

        }


        toNextStageTimer = gameObject.AddComponent<Timer>();
        toNextStageTimer.SetTimer(timeToNextStage);
        toNextStageTimer.StartTimer();
        NextStage();
    }

    private void LateUpdate()
    {
        if (toNextStageTimer.GetStatus() == Timer.Status.FINISHED)
        {
            NextStage();
        }
    }

    public bool NextStage()
    {

        if (currentStageIndex == 0)
        {
            currentStage.GetComponent<FirstPlantStage>().Drop();
        }

        currentStageIndex++;
        if (currentStageIndex >= stages.Count) return false;

        if (currentStage != null)
        {
            currentStage.gameObject.SetActive(false);
        }
        currentStage = stages[currentStageIndex];
        currentStage.gameObject.SetActive(true);

        Debug.Log("Next stage:" + currentStage.name);

        toNextStageTimer.ResetTimer();
        toNextStageTimer.StartTimer();

        return true;
    }

    public void SetTimeToNextStage(float time)
    {
        toNextStageTimer.SetTimer(timeToNextStage);
        toNextStageTimer.StartTimer();
    }

}
