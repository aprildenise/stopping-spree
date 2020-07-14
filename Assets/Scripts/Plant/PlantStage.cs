using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlantStage : MonoBehaviour, Interactible
{

    [HideInInspector] public PlantStageController stageController;
    [HideInInspector] public SpriteRenderer sprite;

    public void InteractWith()
    {
        //stageController.parentPlant.InteractWith();

        Debug.Log("Player interact with:" + gameObject.name);
        GameObject playeritem = PlayerController.instance.currentlyHolding;
        if (playeritem == null) return;
        if (playeritem.GetComponent<WateringCan>())
        {
            // TODO: IS THIS APPROPRIATE?
            Destroy(playeritem.gameObject);
            PlayerController.instance.currentlyHolding = null;
            stageController.parentPlant.ResetHealth();
        }

        OnInteractWith();

    }

    public virtual void OnInteractWith()
    {
        return;
    }

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }



}
