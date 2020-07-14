using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trader : Person
{

    public int numFruitBuying;
    public int fruitPrice;

    public override void InteractWith()
    {
        if (PlayerController.instance.fruit >= numFruitBuying)
        {
            PlayerController.instance.fruit -= numFruitBuying;
            PlayerController.instance.money += fruitPrice;
        }
    }

    public void OnYes()
    {

    }

    public void OnNo()
    {

    }
}
