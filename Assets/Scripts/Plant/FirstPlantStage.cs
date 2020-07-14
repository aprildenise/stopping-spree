using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPlantStage : PlantStage
{


    public float maxHealth;
    public float timeUntilNextRequest;
    private Timer requestTimer;

    public string currentlyRequesting; 
    public static readonly string water = "water";
    public static readonly string dirt = "dirt";
    public SpriteRenderer needs;

    private void Start()
    {
        stageController.parentPlant.SetMaxHealth(maxHealth);
        requestTimer = gameObject.AddComponent<Timer>();
        requestTimer.SetTimer(timeUntilNextRequest);
        requestTimer.StartTimer();
    }

    private void LateUpdate()
    {
        if (requestTimer.GetStatus() == Timer.Status.FINISHED)
        {
            RequestTools();
            requestTimer.ResetTimer();
        }
    }

    public override void OnInteractWith()
    {
        if (!GiveTools())
        {

        }
    }

    private void RequestTools()
    {
        Debug.Log(stageController.parentPlant.gameObject.name + ":Requesting tools");
        stageController.parentPlant.requestAnim.SetBool("isRequesting", true);
        stageController.parentPlant.decreaseHealthOverTime = true;

        int random = Random.Range(1, 3);
        if (random == 1)
        {
            currentlyRequesting = water;
        }
        else
        {
            currentlyRequesting = dirt;
        }
        stageController.parentPlant.requestAnim.SetInteger("requestItem", random);
    }

    private bool PickUp()
    {
        if (PlayerController.instance.currentlyHolding != null) return false;

        transform.SetParent(PlayerController.instance.transform);
        transform.position = PlayerController.instance.overHeadPosition.transform.position;
        PlayerController.instance.currentlyHolding = this.gameObject;

        return true;
    }

    public void Drop()
    {
        transform.SetParent(PrefabManager.instance.transform);
        PlayerController.instance.currentlyHolding = null;
        transform.position = PlayerController.instance.transform.position;
    }

    public bool GiveTools()
    {
        if (PlayerController.instance.currentlyHolding == null) return false;

        if ((currentlyRequesting == water && PlayerController.instance.currentlyHolding.GetComponent<WateringCan>())
            || currentlyRequesting == dirt && PlayerController.instance.currentlyHolding.GetComponent<Dirt>())
        {

            Debug.Log(stageController.parentPlant.gameObject.name + ":Tools given.");
            stageController.parentPlant.ResetHealth();
            requestTimer.ResetTimer();
            requestTimer.StartTimer();

            stageController.parentPlant.requestAnim.SetBool("isRequesting", false);
            stageController.parentPlant.decreaseHealthOverTime = false;
            return true;
        }

        return false;

    }

}
