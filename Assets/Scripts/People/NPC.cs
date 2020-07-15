using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NPC : People
{
    
    [Header("NPC")]
    public Store currentStore;
    public Collectible currentCollectible;
    public float timeout;
    public Vector3 target;
    private float originalSpeed;

    // State controls.
    private readonly string chooseStoreState = "ChooseStore";
    private readonly string toStoreState = "ToStore";
    private readonly string chooseCollectibleState = "ChooseCollectible";
    private readonly string toCollectibleState = "ToCollectible";
    private readonly string chooseInteractState = "chooseInteract";
    private string currentState;

    private NavMeshAgent agent;
    private Timer timeoutTimer;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        timeoutTimer = gameObject.AddComponent<Timer>();
        timeoutTimer.SetTimer(timeout);
        timeoutTimer.allowUnscaledDeltaTime = true;
        timeoutTimer.StartTimer();

        originalSpeed = agent.speed;
        currentState = chooseStoreState;
    }

    protected override void OnLateUpdate()
    {

        isMoving = !agent.isStopped;
        moveVelocity = agent.velocity;
        Debug.Log(moveVelocity);
        isDiagonal = (moveVelocity.z > 0 && moveVelocity.x < 0)
            || (moveVelocity.z > 0 && moveVelocity.x > 0)
            || (moveVelocity.z < 0 && moveVelocity.x < 0)
            || (moveVelocity.z < 0 && moveVelocity.x > 0);

        Debug.Log(agent.isStopped + ":" + gameObject.name);

        // Timeout the NPC if the agent seems stuck somewhere.
        if ((currentState.Equals(toStoreState) || currentState.Equals(toCollectibleState))
            && timeoutTimer.GetStatus() == Timer.Status.FINISHED)
        {
            currentState = chooseStoreState;
            timeoutTimer.StartTimer();
        }

        // Choose what to do based on the current state this NPC is in.
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
        int enterOrLeave = Random.Range(0, 101);
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
        int stayOrLeave = Random.Range(0, 101);
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
            int takeOrThrow = Random.Range(0, 101);
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
