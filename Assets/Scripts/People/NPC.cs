using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NPC : People
{
    private NavMeshAgent agent;

    public Store currentStore;
    public Collectible currentCollectible;
    private float originalSpeed;
    private readonly string chooseStoreState = "ChooseStore";
    private readonly string toStoreState = "ToStore";
    private readonly string chooseCollectibleState = "ChooseCollectible";
    private readonly string toCollectibleState = "ToCollectible";
    private readonly string chooseInteractState = "chooseInteract";
    private string currentState;
    
    public Vector3 target;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        originalSpeed = agent.speed;
        currentState = chooseStoreState;
    }

    protected override void OnLateUpdate()
    {

        isMoving = !agent.isStopped;

        Debug.Log(agent.isStopped + ":" + gameObject.name);

        if (currentState.Equals(chooseStoreState))
        {
            OnChooseStore();
        }
        else if (currentState.Equals(toStoreState))
        {
            if (!agent.pathPending)
            {
                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                    {
                        OnToStore();
                    }
                }
            }
        }
        else if (currentState.Equals(chooseCollectibleState))
        {
            OnChooseCollectible();
        }
        else if (currentState.Equals(toCollectibleState))
        {
            if (!agent.pathPending)
            {
                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                    {
                        OnToCollectible();
                    }
                }
            }
        }
        else if (currentState.Equals(chooseInteractState))
        {
            OnChooseInteract();
        }
    }

    private void OnChooseStore()
    {
        currentStore = GameManager.instance.GetTargetStore();
        target = currentStore.GetEntrance();
        agent.SetDestination(target);

        currentState = toStoreState;
    }

    private void OnToStore()
    {

        agent.ResetPath();
        int enterOrLeave = Random.Range(0, 100);
        if (enterOrLeave < 100)
        {
            currentState = chooseCollectibleState;
        }
        else
        {
            currentState = chooseStoreState;
        }
    }

    private void OnChooseCollectible()
    {
        if (currentStore.GetNumCollectibles() <= 0)
        {
            currentState = chooseStoreState;
            return;
        }
        int stayOrLeave = Random.Range(0, 100);
        if (stayOrLeave < 100)
        {
            currentCollectible = currentStore.GetTargetCollectible();
            if (currentCollectible == null)
            {
                currentState = chooseCollectibleState;
                return;
            }
            target = currentCollectible.transform.position;
            agent.SetDestination(target);
            currentState = toCollectibleState;
        }
        else
        {
            currentState = chooseStoreState;
        }

    }

    private void OnToCollectible()
    {
        agent.ResetPath();
        currentState = chooseInteractState;
    }

    private void OnChooseInteract()
    {
        if (currentCollectible != null)
        {
            int takeOrThrow = Random.Range(0, 100);
            if (takeOrThrow < 100)
            {
                currentStore.RemoveCollectible(currentCollectible);
            }
            else
            {
                currentCollectible.transform.position = transform.position;
            }
        }
        currentState = chooseCollectibleState;
    }

    private void OnTriggerStay(Collider other)
    {

        try
        {
            NavMeshAgent otherAgent = other.GetComponent<NavMeshAgent>();
            if (otherAgent.speed >= agent.speed)
            {
                agent.speed = originalSpeed / 3;
            }
            if (otherAgent.speed < agent.speed)
            {
                agent.speed = originalSpeed * 1.5f;
            }
            else
            {
                agent.speed = 1f;
            }
        }
        catch (System.Exception)
        {


        }
    }

    private void OnTriggerExit(Collider other)
    {
        try
        {

            NavMeshAgent otherAgent = other.GetComponent<NavMeshAgent>();
            agent.speed = originalSpeed;
            agent.SetDestination(target);
        }
        catch (System.Exception)
        {


        }
    }

}
