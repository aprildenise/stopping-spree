using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ToolTip : MonoBehaviour
{

    public CanvasGroup group;
    public Image itemImage;
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
        SetAlpha(0);
    }

    public void SetToolTip(Sprite sprite, float cost, float value, string name, string description)
    {
        itemImage.sprite = sprite;
        //itemImage.SetNativeSize();
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
