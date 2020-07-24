using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider))]
public class InventorySlot : MonoBehaviour
{
    public Color occupiedColor;
    public Color overlappingColor;
    public Color defaultColor;
    public int x;
    public int y;

    public static readonly float slotSize = 9;
    public InventoryManager parent { get; private set; }

    /// <summary>
    /// Image component of this sprite to change the colors dynamically.
    /// </summary>
    private Image image;

    [HideInInspector] public GameObject occupyingObject;

    private void Awake()
    {
        // Set the components.
        image = GetComponent<Image>();
        image.color = defaultColor;
        parent = transform.parent.transform.parent.GetComponent<InventoryManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // If there is no object in this slot, turn blue. If there is, turn overlappingColor
        if (occupyingObject == null) image.color = occupiedColor;
        else image.color = overlappingColor;
        
    }


    private void OnTriggerExit(Collider other)
    {
        // If the object that exited was the occupyingObject, become unoccupied.
        if (other.gameObject.Equals(occupyingObject))
        {
            occupyingObject = null;
            
        }

        // If the object that exited was something else, default to occupied if occupied, or default if unoccupied.
        if (occupyingObject == null)
        {
            image.color = defaultColor;
        }
        else
        {
            image.color = occupiedColor;
        }
    }


}
