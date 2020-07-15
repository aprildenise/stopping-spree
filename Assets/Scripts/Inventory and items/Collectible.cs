using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{

    public string itemName;
    public string itemDescription;
    public float cost;
    public float value;
    public int width;
    public float height;
    public bool isExposed = false;
    public bool inInventory = false;
    public bool isPurchased = false;

    private Rigidbody rb;
    private DragAndDrop3D drag;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        drag = GetComponent<DragAndDrop3D>();
    }


    public void Toss()
    {
        gameObject.layer = LayerMask.NameToLayer("Items In Wild");
        SpriteRenderer sprite = gameObject.GetComponent<SpriteRenderer>();
        sprite.sortingOrder = 0;

        InventoryManager.instance.RemoveCollectible(this);
        transform.position = PlayerController.instance.transform.position;
        rb.AddForce(Vector3.up, ForceMode.Impulse);
    }


}
