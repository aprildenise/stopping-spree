using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider))]
public class InventorySlot : MonoBehaviour
{

    public InventoryManager parent { get; private set; }
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
        Debug.Log("inventory slot collision with:" + other.gameObject.name);


        //Vector3 originalPosition = other.transform.position;
        //Vector3 movePosition = new Vector3(originalPosition.x, originalPosition.y + 10f, originalPosition.z);
        //other.gameObject.transform.position = movePosition;

        image.color = Color.gray;
    }

    private void OnTriggerStay(Collider other)
    {

        try
        {
            if (!other.GetComponent<DragAndDrop3D>().mouseDrag && other.GetComponent<Collectible>() != null)
            {
                parent.AddCollectible(other.GetComponent<Collectible>());
                placedCollectible = other.gameObject;

                Vector3 snap = transform.position;
                placedCollectible.transform.position = snap;
            }
        }
        catch (System.NullReferenceException)
        {

        }
    }


    private void OnTriggerExit(Collider other)
    {

        //Vector3 originalPosition = other.transform.position;
        //Vector3 movePosition = new Vector3(originalPosition.x, originalPosition.y - 10f, originalPosition.z);
        //other.gameObject.transform.position = movePosition;

        image.color = Color.white;

        placedCollectible = this.gameObject;
    }


}
