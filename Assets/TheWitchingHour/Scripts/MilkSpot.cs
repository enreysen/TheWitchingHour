using UnityEngine;

public class MilkSpot : MonoBehaviour
{
    public CooldownVisual cooldown_visual;
    public float seconds = 1.5f; // milk fastest

    public void OnCupPlaced(CupState cup)
        {
            if (cup.contents != CupContents.Empty) return;
            Debug.Log("has milk");
            cup.LockFor(seconds);
            cooldown_visual.ShowVisual(seconds);

            cup.contents = CupContents.Milk;
            cup.ApplyVisuals();
        }

}