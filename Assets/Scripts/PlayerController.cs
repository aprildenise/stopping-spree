using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{


    private Rigidbody rigidBody;

    public float moveSpeed = 10f;
    public bool isMoving { get;  private set; }
    private Vector3 moveVelocity;

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
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get axis input for movement.
        Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput.normalized * moveSpeed;
        isMoving = moveInput != Vector3.zero;

    }

    private void FixedUpdate()
    {
        // Move the object based on the movement input
        rigidBody.MovePosition(rigidBody.position + moveVelocity * Time.fixedDeltaTime);

    }

    private void LateUpdate()
    {
        SplatMapController.instance.SetTile(transform.position);
    }
}
