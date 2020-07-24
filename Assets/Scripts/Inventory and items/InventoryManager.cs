using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Xml.Schema;

public class InventoryManager : MonoBehaviour
{
    private List<Collectible> collectibles;

    private float totalCost = 0f;
    private float totalValue = 0f;
    public TextMeshProUGUI totalCostText;
    public TextMeshProUGUI totalValueText;

    public static InventoryManager instance { get; private set; }
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        collectibles = new List<Collectible>();
    }


    public bool AddCollectible(Collectible collectible)
    {
        if (collectibles.Contains(collectible)) return false;

        // Update the values of the collectible and the inventory.
        collectibles.Add(collectible);
        totalCost += collectible.cost;
        totalValue += collectible.value;
        collectible.isExposed = true;
        collectible.inInventory = true;
        UpdateText(totalCost, totalValue);

        // Fix the sorting layer of the collectible sprite so that it doesn't overlap with anything.
        collectible.sprite.sortingOrder = 11;

        // All good!
        return true;
    }

    public bool RemoveCollectible(Collectible collectible)
    {
        if (!collectibles.Remove(collectible)) return false;
        totalCost -= collectible.cost;
        totalValue -= collectible.value;
        collectible.inInventory = false;
        UpdateText(totalCost, totalValue);

        collectible.follow.enabled = false;

        return true;
    }

    public void UpdateText(float cost, float value)
    {
        totalCostText.SetText("$" + (float)Mathf.Round(cost * 100f) / 100f);
        totalValueText.SetText("" + value);
    }

    public bool HasUnpurchasedCollectible()
    {
        foreach (Collectible c in collectibles)
        {
            if (!c.isPurchased) return true;
        }
        return false;
    }

    public void PurchaseAllItems()
    {
        foreach(Collectible c in collectibles)
        {
            c.isPurchased = true;
        }
    }




}
