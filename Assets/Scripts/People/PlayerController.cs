using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : People
{


    private Rigidbody rigidBody;

    public float moveSpeed = 10f;

    public static PlayerController instance { get; private set; }
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
    }

    // Start is called before the first frame update
    protected override void OnStart()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update()
    {
        // Get axis input for movement.
        Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput.normalized * moveSpeed;
        isMoving = moveInput != Vector3.zero;
        isDiagonal = Mathf.Abs(moveInput.x) == Mathf.Abs(moveInput.z) && moveInput.x != 0 && moveInput.z != 0;

    }

    private void FixedUpdate()
    {
        // Move the object based on the movement input
        //rigidBody.MovePosition(rigidBody.position + moveVelocity * Time.fixedDeltaTime);
        if (isMoving)
        {
            rigidBody.MovePosition(rigidBody.position + moveVelocity * Time.fixedDeltaTime);
            //rigidBody.MovePosition(moveVelocity * Time.fixedDeltaTime);
        }

    }


}
