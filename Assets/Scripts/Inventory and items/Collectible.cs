using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Marks the object as a Collectible. Must have ShowToolTip and DragAndDrop3D components in its child to
/// be fully functional!
/// </summary>
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

    private bool appearInWorld = true;

    // Components.
    [HideInInspector] public SpriteRenderer sprite { get; private set; }
    [HideInInspector] public ObjectFollow follow { get; private set; }

    private void Start()
    {
        // Get component.
        sprite = GetComponent<SpriteRenderer>();
        follow = GetComponent<ObjectFollow>();
        follow.enabled = false;

        // Make sure this starts with the correct scale.
        transform.localScale = new Vector3(5, 5, 5);

    }


    /// <summary>
    /// Change the appearance so that it's appropriate for the inventory.
    /// </summary>
    public void AppearInInventory()
    {
        if (!appearInWorld) return;
        appearInWorld = false;
        gameObject.layer = LayerMask.NameToLayer("Items In Inventory");
        foreach (Transform child in transform)
        {
            child.gameObject.layer = LayerMask.NameToLayer("Items In Inventory");
        }
        sprite.sortingOrder = 12;
        transform.localScale *= 2;
    }

    /// <summary>
    /// Change the appearance so that it's appropriate for the game world.
    /// </summary>
    public void AppearInWorld()
    {
        if (appearInWorld) return;
        appearInWorld = true;
        gameObject.layer = LayerMask.NameToLayer("World");
        foreach (Transform child in transform)
        {
            child.gameObject.layer = LayerMask.NameToLayer("World");
        }
        sprite.sortingOrder = 0;
        transform.localScale /= 2;
    }

    /// <summary>
    /// 
    /// </summary>
    public void Toss()
    {
        AppearInWorld();

        InventoryManager.instance.RemoveCollectible(this);
        transform.position = PlayerController.instance.transform.position;
    }


    //private void OnValidate()
    //{
    //    gameObject.name = itemName;
    //}

}
