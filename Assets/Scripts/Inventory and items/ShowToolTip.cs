using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Show the Tool Tip UI whenever the mouse pointer is over this object. This component MUST be a child of the Collectible object
/// and MUST have a Collider.
/// </summary>
[RequireComponent(typeof(BoxCollider))]
public class ShowToolTip : MonoBehaviour
{

    // Components.
    private ToolTip toolTip;
    /// <summary>
    /// The Collectible object that this ShowToolTip belongs to. This should be in the parent transform.
    /// </summary>
    private Collectible collectible;

    private bool mouseDrag = false;

    private void Start()
    {
        // Cache the components.
        toolTip = ToolTip.instance;
        collectible = transform.parent.GetComponent<Collectible>();
    }

    /// <summary>
    /// Show the Tool Tip UI.
    /// </summary>
    private void OnMouseOver()
    {
        if (mouseDrag) return;
        toolTip.SetAlpha(1f);
        toolTip.SetPosition(Input.mousePosition);
        toolTip.SetToolTip(collectible.sprite.sprite, collectible.cost, collectible.value,
            collectible.itemName, collectible.itemDescription);
    }

    /// <summary>
    /// Hide the Tool Tip UI.
    /// </summary>
    private void OnMouseExit()
    {
        toolTip.SetAlpha(0f);
    }

    /// <summary>
    /// Detect any mouse drags. Hide the Tool Tip UI on mouse drags.
    /// </summary>
    private void OnMouseDrag()
    {
        mouseDrag = true;
        toolTip.SetAlpha(0f);
    }

    /// <summary>
    /// Detect when a mouse drag has been finished.
    /// </summary>
    private void OnMouseUp()
    {
        if (mouseDrag) mouseDrag = false;
    }

}
