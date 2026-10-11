using System.Collections;
using UnityEngine;

public class CoffeeSpot : MonoBehaviour
{
    public void OnCupPlaced(CupState cup)
    {
        if (cup.contents != CupContents.Empty) return;
        Debug.Log("has coffee");
        cup.contents = CupContents.Coffee;
    }
}