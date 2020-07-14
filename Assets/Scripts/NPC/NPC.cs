using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class NPC : MonoBehaviour, Interactible
{

    public bool isMoving;

    public Animator anim;
    public NavMeshAgent agent;

    protected void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    protected void LateUpdate()
    {
        isMoving = agent.velocity.magnitude > 0;
        anim.SetBool("isMoving", isMoving);
    }


    public abstract void InteractWith();
}
