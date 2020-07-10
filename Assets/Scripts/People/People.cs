using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class People : MonoBehaviour
{

    public bool isExposed { get; protected set; }
    public bool isMoving { get; protected set; }
    public bool isDiagonal { get; protected set; }
    public Vector3 moveVelocity { get; protected set; }
    public SpriteRenderer flip1;
    public SpriteRenderer flip2;
    protected Animator anim;

    protected void Start()
    {
        anim = GetComponent<Animator>();
        OnStart();
    }

    protected virtual void OnStart()
    {
        return;
    }

    protected void LateUpdate()
    {

        if (isMoving)
        {
            SplatMapController.instance.SetTile(transform.position);
        }


        try
        {
            if (moveVelocity.x < 0)
            {
                flip1.flipX = true;
                flip2.flipX = true;
            }
            else if (moveVelocity.x > 0)
            {
                flip1.flipX = false;
                flip2.flipX = false;
            }
        } catch (System.Exception)
        {

        }

        anim.SetBool("isMoving", isMoving);
        anim.SetBool("isDiagonal", isDiagonal);
        anim.SetFloat("horizontal", moveVelocity.x);
        anim.SetFloat("vertical", moveVelocity.z);


        OnLateUpdate();
    }

    protected virtual void OnLateUpdate()
    {
        return;
    }
}
