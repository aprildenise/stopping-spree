using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ToolTip : MonoBehaviour
{

    public CanvasGroup group;
    public Image image;
    public TextMeshProUGUI itemCost;
    public TextMeshProUGUI itemValue;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemDescription;

    public static ToolTip instance { get; private set; }

    private RectTransform rect;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        rect = GetComponent<RectTransform>();
    }

    public void SetToolTip(Sprite sprite, float cost, float value, string name, string description)
    {
        image.sprite = sprite;
        itemCost.SetText(cost + "");
        itemValue.SetText(value + "");
        itemName.SetText(name);
        itemDescription.SetText(description);

    }

    public void SetAlpha(float alpha)
    {
        group.alpha = alpha;
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }


}
