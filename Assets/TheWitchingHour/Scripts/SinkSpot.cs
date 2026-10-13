using System.Collections;
using UnityEngine;

public class SinkSpot : MonoBehaviour
{
    public void OnCupPlaced(CupState cup)
    {
        Debug.Log("has rinsed");
        cup.contents = CupContents.Empty;
        cup.ApplyVisuals();
    }
}