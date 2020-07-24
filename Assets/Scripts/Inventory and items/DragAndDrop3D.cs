using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles dragging and dropping. This component MUST be a child of the Collectible object
/// and MUST have a Collider.
/// </summary>
[RequireComponent(typeof(BoxCollider))]
public class DragAndDrop3D : MonoBehaviour
{
    public LayerMask collideWith;

    /// <summary>
    /// The Collectible object that this DragAndDrop3D belongs to.
    /// </summary>
    private Collectible collectible;
    /// <summary>
    /// The parent transform of this object. This is what is actually moved during dragging and dropping.
    /// </summary>
    private Transform parent;
    private new BoxCollider collider;

    // For dragging calculations.
    private Vector3 mousePosition;
    private Vector3 smoothPosition;
    public bool mouseDrag { get; private set; }
    private bool inInventoryMenu = false;
    private bool overlappingCollectible = false;
    private Vector3 scaledColliderSize;
    private bool appearInWorld;

    private void Awake()
    {
        // Get the components.
        parent = transform.parent.transform;
        collectible = parent.GetComponent<Collectible>();
        collider = GetComponent<BoxCollider>();

        // Make sure the size of the collider is at least 5 on z.
        Vector3 size = collider.size;
        size.z = 5f;
        collider.size = size;
        collider.isTrigger = true;

        // Get the size of the collider in order to raycast.
        scaledColliderSize = new Vector3(Mathf.Abs(transform.TransformVector(collider.size).x), 
            Mathf.Abs(transform.TransformVector(collider.size).y),
            Mathf.Abs(transform.TransformVector(collider.size).z));
    }

    private void OnMouseDown()
    {
        // If the collectible is snapped to follow an object, undo that.
        collectible.follow.enabled = false;

        // Calculate the smooth position in case the player begins dragging after this mouse down.
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        smoothPosition = mousePosition - transform.position;
    }

    private void OnMouseDrag()
    {
        // Make the item visible and large for the player
        collectible.AppearInInventory();

        // Move the parent transform to whereever the player drags to, smoothly.
        mouseDrag = true;
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 dragPosition = new Vector3(mousePosition.x - smoothPosition.x, 0f, mousePosition.z - smoothPosition.z);
        parent.transform.position = dragPosition;

    }

    /// <summary>
    /// Handle what happens when the player stops dragging an object. 
    /// If they drop it outside of the inventory menu, then we Toss().
    /// Else, we add it to the inventory.
    /// </summary>
    private void OnMouseUp()
    {
        mouseDrag = false;

        // If this object is dropped outside the inventory menu, use the Toss() to handle it. 
        if (!inInventoryMenu || overlappingCollectible)
        {
            collectible.Toss();
            return;
        }

        // Snap the object into the correct place of the inventory slot.
        Collider[] hits = Physics.OverlapBox(gameObject.transform.position, scaledColliderSize / 2, Quaternion.identity, collideWith);
        if (hits.Length == 0) return; //TODO handle this better.
        Collider snapTo = hits[0];
        foreach (Collider hit in hits)
        {
            // Mark these hits as occupied.
            hit.GetComponent<InventorySlot>().occupyingObject = this.gameObject;

            if (hit.transform.position.x <= snapTo.transform.position.x && hit.transform.position.z >= snapTo.transform.position.z)
            {
                snapTo = hit;
            }
        }

        // Calculate the position to snap to, using the top left most Inventory Slot that this is colliding with.
        Vector3 offset = new Vector3(InventorySlot.slotSize * collectible.width / 2, 0, -1 * (InventorySlot.slotSize * collectible.height / 2));
        parent.transform.position = snapTo.transform.position + offset;

        // Keep this collectible in place as the inventory menu moves.
        collectible.follow.enabled = true;
        collectible.follow.target = snapTo.gameObject.transform;
        collectible.follow.offset = offset;

        // Add this to the inventory.
        InventoryManager.instance.AddCollectible(collectible);
    }


    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Sees " + other.gameObject.name);

        // If what entered was a collectible, mark that this is overlapping.
        if (other.CompareTag("Collectible")) overlappingCollectible = true;

        // If what was entered was the inventory menu, mark that.
        if (other.CompareTag("Inventory Menu")) inInventoryMenu = true;

    }

    private void OnTriggerExit(Collider other)
    {
        // If what entered was a collectible, mark that this is overlapping.
        if (other.CompareTag("Collectible")) overlappingCollectible = false;

        // If what was entered was the inventory menu, mark that.
        if (other.CompareTag("Inventory Menu")) inInventoryMenu = false;
    }

    private void OnDrawGizmos()
    {
        // Draw a yellow cube at the transform position
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, scaledColliderSize);
    }

}
