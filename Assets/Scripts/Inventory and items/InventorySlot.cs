using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider))]
public class InventorySlot : MonoBehaviour
{

    public int x;
    public int y;

    public InventoryManager parent { get; private set; }
    public bool seesCollectible { get; private set; }
    private Image image;

    private GameObject placedCollectible;

    private void Start()
    {
        image = GetComponent<Image>();
        parent = transform.parent.transform.parent.GetComponent<InventoryManager>();
        placedCollectible = this.gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        seesCollectible = true;
        image.color = Color.gray;
    }

    private void OnTriggerStay(Collider other)
    {

        try
        {
            if (!other.GetComponent<DragAndDrop3D>().mouseDrag && other.GetComponent<Collectible>() != null)
            {
                if (other.GetComponent<Collectible>().width == 1 && other.GetComponent<Collectible>().height == 1)
                {
                    parent.AddCollectible(other.GetComponent<Collectible>());
                    placedCollectible = other.gameObject;

                    Vector3 snap = transform.position;
                    placedCollectible.transform.position = snap;
                }
            }
        }
        catch (System.NullReferenceException)
        {

        }
    }


    private void OnTriggerExit(Collider other)
    {
        seesCollectible = false;
        image.color = Color.white;
        placedCollectible = this.gameObject;
    }


}
