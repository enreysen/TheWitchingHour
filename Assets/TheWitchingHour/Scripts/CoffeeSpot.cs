using System.Collections;
using UnityEngine;

public class CoffeeSpot : MonoBehaviour
{
    public CooldownVisual cooldown_visual;
    public float seconds = 3f;

    public void OnCupPlaced(CupState cup)
    {
        if (cup.contents != CupContents.Empty) return;
        cup.LockFor(seconds);
        cooldown_visual.ShowVisual(seconds);

        Debug.Log("coffee");
        cup.contents = CupContents.Coffee;
        cup.ApplyVisuals();
    }
}