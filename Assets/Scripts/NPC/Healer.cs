using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healer : Person
{

    public float timeToHeal;
    private Timer healCooldown;

    private void Start()
    {
        healCooldown = gameObject.AddComponent<Timer>();
        healCooldown.SetTimer(timeToHeal, Timer.Status.FINISHED);
    }

    public override void InteractWith()
    {
        if (healCooldown.GetStatus() != Timer.Status.FINISHED) return;
        agent.isStopped = true;
        PlayerController.instance.ResetHealth();
        agent.isStopped = false;
        healCooldown.ResetTimer();
        healCooldown.StartTimer();
    }
}
