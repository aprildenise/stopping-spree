using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Person : NPC
{

    protected void Update()
    {
        if (agent.remainingDistance < 5f)
        {
            agent.SetDestination(WorldDimensions.instance.GetRandomWorldPoint());
        }
    }

}
