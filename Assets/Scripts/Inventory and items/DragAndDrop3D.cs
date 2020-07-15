using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class DragAndDrop3D : MonoBehaviour
{

    public LayerMask collideWith;

    private Collectible collectible;
    private new BoxCollider collider;

    private Vector3 mousePosition;
    private Vector3 smoothPosition;
    public bool mouseDrag { get; private set; }
    private bool inInventoryMenu = false;
    private bool onOtherCollectible = false;
    private Vector3 scaledColliderSize;

    private void Awake()
    {
        collectible = GetComponent<Collectible>();
        collider = GetComponent<BoxCollider>();
        //Debug.Log(transform.TransformVector(collider.size));
        scaledColliderSize = new Vector3(Mathf.Abs(transform.TransformVector(collider.size).x), 
            Mathf.Abs(transform.TransformVector(collider.size).y),
            Mathf.Abs(transform.TransformVector(collider.size).z));
    }

    private void OnMouseDown()
    { 
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        smoothPosition = mousePosition - transform.position;
    }

    private void OnMouseDrag()
    {
        gameObject.layer = LayerMask.NameToLayer("Items In Inventory");
        SpriteRenderer sprite = gameObject.GetComponent<SpriteRenderer>();
        sprite.sortingOrder = 2;

        mouseDrag = true;
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 dragPosition = new Vector3(mousePosition.x - smoothPosition.x, 0f, mousePosition.z - smoothPosition.z);
        transform.position = dragPosition;

        Collider[] hits = Physics.OverlapBox(gameObject.transform.position, scaledColliderSize, Quaternion.identity, collideWith);
        foreach (Collider hit in hits)
        {
            Debug.Log("Collide with:" + hit.gameObject.name);
        }

    }

    private void OnMouseUp()
    {
        mouseDrag = false;

        if (!inInventoryMenu || onOtherCollectible)
        {
            collectible.Toss();
            return;
        }

        if (collectible.width > 1 || collectible.height > 1)
        {
            Collider[] hits = Physics.OverlapBox(gameObject.transform.position, scaledColliderSize, Quaternion.identity, collideWith);
            Collider snapTo = hits[0];
            foreach (Collider hit in hits)
            {
                //if (!hit.gameObject.GetComponent<InventorySlot>().seesCollectible) continue;

                Debug.Log("hit:" + hit.gameObject.transform.position + " " + hit.gameObject.transform.name);
                if (hit.transform.position.x <= snapTo.transform.position.x && hit.transform.position.z >= snapTo.transform.position.z)
                {
                    Debug.Log("choose:" + hit.gameObject.transform.position + " " + hit.gameObject.transform.name);
                    snapTo = hit;
                }
                //            if (hit.GetComponent<InventorySlot>().x <= snapTo.GetComponent<InventorySlot>().x
                //&& hit.GetComponent<InventorySlot>().z = snapTo.GetComponent<InventorySlot>().x)
                //            {
                //                snapTo = hit;
                //            }
            }



            //transform.position = snapTo.transform.position + new Vector3(2f, 0f, -2f);
            transform.position = snapTo.transform.position;
            InventoryManager.instance.AddCollectible(collectible);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        try
        {
            if (other.GetComponent<Collectible>())
            {
                onOtherCollectible = true;
            }
            if (other.GetComponent<InventoryManager>())
            {
                inInventoryMenu = true;
            }
        }
        catch (System.NullReferenceException)
        {

        }
    }

    private void OnTriggerStay(Collider other)
    {
        try
        {
            if (other.GetComponent<Collectible>())
            {
                onOtherCollectible = true;
            }
            if (other.GetComponent<InventoryManager>())
            {
                inInventoryMenu = true;
            }
        }
        catch (System.NullReferenceException)
        {

        }
    }

    private void OnTriggerExit(Collider other)
    {
        try
        {
            if (other.GetComponent<Collectible>())
            {
                onOtherCollectible = false;
            }
            if (other.GetComponent<InventoryManager>())
            {
                inInventoryMenu = false;
            }
        }
        catch (System.NullReferenceException)
        {

        }
    }

}
