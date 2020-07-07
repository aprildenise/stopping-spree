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

        collectibles.Add(collectible);
        totalCost += collectible.cost;
        totalValue += collectible.value;
        collectible.isExposed = true;
        collectible.inInventory = true;
        UpdateText(totalCost, totalValue);
        return true;
    }

    public bool RemoveCollectible(Collectible collectible)
    {
        if (!collectibles.Remove(collectible)) return false;
        totalCost -= collectible.cost;
        totalValue -= collectible.value;
        collectible.inInventory = false;
        UpdateText(totalCost, totalValue);
        return true;
    }

    public void UpdateText(float cost, float value)
    {
        totalCostText.SetText("$" + (float)Mathf.Round(cost * 100f) / 100f);
        totalValueText.SetText("" + value);
    }




}
