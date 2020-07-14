using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Supplier: Person
{

    [SerializeField]
    public GameObject toolSuppling;
    public int toolPrice;

    public override void InteractWith()
    {

        //DialogueManager.instance.DisplayDialogue

        try
        {
            if (toolSuppling == null) return;
            if (PlayerController.instance.currentlyHolding != null) return;
            if (PlayerController.instance.money < toolPrice) return;

            agent.isStopped = true;
            Debug.Log("NPC Interaction:" + gameObject.name);
            GameObject tool = Instantiate(toolSuppling,
                PlayerController.instance.overHeadPosition.position,
                toolSuppling.transform.rotation,
                PlayerController.instance.transform);
            tool.transform.localScale = new Vector3(1, 1, 1);
            PlayerController.instance.currentlyHolding = tool;
            PlayerController.instance.money -= toolPrice;
            agent.isStopped = false;
        } catch (System.Exception)
        {

        }

    }


    public void OnYes()
    {

    }

    public void OnNo()
    {

    }


}
