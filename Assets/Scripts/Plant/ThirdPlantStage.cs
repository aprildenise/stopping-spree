using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ThirdPlantStage : SpawningStage
{
    public float aggroRadius;
    public float attackCooldown;
    protected bool findTriggers = false;


    private Animator anim;
    private BoxCollider hitBox;
    private Timer attackCooldownTimer;


    protected override void OnStart()
    {
        anim = GetComponent<Animator>();
        hitBox = GetComponent<BoxCollider>();
        attackCooldownTimer = gameObject.AddComponent<Timer>();
        attackCooldownTimer.SetTimer(attackCooldown);
        attackCooldownTimer.StartTimer();

        stageController.parentPlant.decreaseHealthOverTime = false;
    }

    protected void Update()
    {


        if (attackCooldownTimer.GetStatus() == Timer.Status.FINISHED)
        {
            Collider[] hits = Physics.OverlapSphere(transform.position, aggroRadius);
            foreach (Collider hit in hits)
            {
                if (hit.gameObject.CompareTag("Player"))
                {
                    anim.SetTrigger("Attack");
                }
            }
        }
    }

    /// <summary>
    /// Called by animator.
    /// </summary>
    public virtual void OnAttack()
    {
        findTriggers = true;
    }

    /// <summary>
    /// Called by animator.
    /// </summary>
    public virtual void OnIdle()
    {
        findTriggers = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!findTriggers) return;
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Destructible>().TakeDamage();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!findTriggers) return;
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Destructible>().TakeDamage();
        }
    }

    void OnDrawGizmosSelected()
    {
        // Display the explosion radius when selected
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, aggroRadius);
    }

}
