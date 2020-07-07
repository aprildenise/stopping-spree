using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class DragAndDrop3D : MonoBehaviour
{

    private Collectible collectible;

    private Vector3 mousePosition;
    private Vector3 smoothPosition;
    public bool mouseDrag { get; private set; }
    private bool inInventoryMenu = false;
    private bool onOtherCollectible = false;

    private void Awake()
    {
        collectible = GetComponent<Collectible>();
    }

    private void OnMouseDown()
    { 
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        smoothPosition = mousePosition - transform.position;
    }

    private void OnMouseDrag()
    {
        mouseDrag = true;
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 dragPosition = new Vector3(mousePosition.x - smoothPosition.x, 0f, mousePosition.z - smoothPosition.z);
        transform.position = dragPosition;
    }

    private void OnMouseUp()
    {
        mouseDrag = false;

        if (!inInventoryMenu || onOtherCollectible) GetComponent<Collectible>().Toss();
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
