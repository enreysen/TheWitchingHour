using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class SinkSpot : MonoBehaviour
{
    public void OnCupPlaced(CupState cup)
    {
        Debug.Log("has rinsed");
        cup.contents = CupContents.Empty;
        cup.toppings = new List<ToppingType>();
        cup.ApplyVisuals();
    }
}