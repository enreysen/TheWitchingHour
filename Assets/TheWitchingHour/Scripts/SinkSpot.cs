using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class SinkSpot : MonoBehaviour
{
    public CupState cupState;
    public void OnCupPlaced(CupState cup)
    {
        Debug.Log("Rinsed Cup");
        cupState.ResetCup();
    }
}