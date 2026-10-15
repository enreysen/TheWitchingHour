using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class SinkSpot : MonoBehaviour
{
    public CupState cupState;
    public CooldownVisual cooldown_visual;
    public float seconds = 3f;

    public void OnCupPlaced(CupState cup)
    {
        cup.LockFor(seconds);
        cooldown_visual.ShowVisual(seconds);

        Debug.Log("washed");
        cupState.ResetCup();
    }
}