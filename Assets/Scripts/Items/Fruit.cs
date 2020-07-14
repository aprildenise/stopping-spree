using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : Item
{
    public override void InteractWith()
    {
        PlayerController.instance.fruit++;
        Destroy(this.gameObject);
    }
}
