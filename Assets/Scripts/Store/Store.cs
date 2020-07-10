using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : MonoBehaviour
{

    public Transform storeEntrace;
    private BoxCollider entireStore;
    private List<Collectible> allCollectibles;
    private bool playerInStore = false;

    private void Awake()
    {
        allCollectibles = new List<Collectible>();
        foreach (Transform child in transform)
        {
            if (child.name == "items")
            {
                foreach(Transform c in child)
                {
                    allCollectibles.Add(c.GetComponent<Collectible>());
                }
                return;
            }
        }
    }

    public Vector3 GetEntrance()
    {
        return (transform.TransformPoint(storeEntrace.localPosition));
    }

    public int GetNumCollectibles()
    {
        return allCollectibles.Count;
    }

    public Collectible GetTargetCollectible()
    {
        int random = Random.Range(0, allCollectibles.Count - 1);
        try
        {
            return allCollectibles[random];
        } catch (System.ArgumentOutOfRangeException)
        {
            return null;
        }
    }

    public bool RemoveCollectible(Collectible item)
    {
        allCollectibles.Remove(item);
        Destroy(item.gameObject);
        return true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInStore = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (InventoryManager.instance.HasUnpurchasedCollectible())
            {
                // Do something.
            }
            playerInStore = false;
        }
    }
}
