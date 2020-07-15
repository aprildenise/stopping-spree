using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class People : MonoBehaviour
{


    [Header("People")]
    public SpriteRenderer[] toFlip;
    public Animator anim;


    // For movement functions and calculations.
    public bool isExposed { get; protected set; }
    public bool isMoving { get; protected set; }
    public bool isDiagonal { get; protected set; }
    public Vector3 moveVelocity { get; protected set; }



    protected void Start()
    {
        OnStart();
    }

    protected virtual void OnStart()
    {
        return;
    }

    protected void LateUpdate()
    {

        OnLateUpdate();

        if (isMoving)
        {
            SplatMapController.instance.SetTile(transform.position);
        }


        try
        {
            if (moveVelocity.x < 0)
            {
                foreach (SpriteRenderer flip in toFlip)
                {
                    flip.flipX = true;
                }
            }
            else if (moveVelocity.x > 0)
            {
                foreach (SpriteRenderer flip in toFlip)
                {
                    flip.flipX = false;
                }
            }
        } catch (System.Exception)
        {

        }

        anim.SetBool("isMoving", isMoving);
        anim.SetBool("isDiagonal", isDiagonal);
        anim.SetFloat("horizontal", moveVelocity.x);
        anim.SetFloat("vertical", moveVelocity.z);


        
    }

    protected virtual void OnLateUpdate()
    {
        return;
    }
}
