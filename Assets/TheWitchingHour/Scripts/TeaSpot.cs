using UnityEngine;

public class TeaSpot : MonoBehaviour
{
    public CooldownVisual cooldown_visual;
    public float seconds = 3f;

    public void OnCupPlaced(CupState cup)
        {
            if (cup.contents != CupContents.Empty) return;
            cup.LockFor(seconds);
            cooldown_visual.ShowVisual(seconds);

            Debug.Log("tea");
            cup.contents = CupContents.Tea;
            cup.ApplyVisuals();
        }

}