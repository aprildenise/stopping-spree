using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collectible))]
[RequireComponent(typeof(BoxCollider))]
public class ShowToolTip : MonoBehaviour
{

    private ToolTip toolTip;
    private Collectible collectible;

    private bool mouseDrag = false;

    private void Start()
    {
        toolTip = ToolTip.instance;
        collectible = GetComponent<Collectible>();
    }

    private void OnMouseOver()
    {
        if (mouseDrag) return;
        toolTip.SetAlpha(1f);
        toolTip.SetPosition(Input.mousePosition);
    }

    private void OnMouseExit()
    {
        toolTip.SetAlpha(0f);
    }

    private void OnMouseDrag()
    {
        mouseDrag = true;
        toolTip.SetAlpha(0f);
    }

    private void OnMouseUp()
    {
        if (mouseDrag) mouseDrag = false;
    }

}
