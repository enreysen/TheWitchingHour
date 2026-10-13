using UnityEngine;

public class MilkSpot : MonoBehaviour
{

    public void OnCupPlaced(CupState cup)
        {
            if (cup.contents != CupContents.Empty) return;
            Debug.Log("has milk");
            cup.contents = CupContents.Milk;
            cup.ApplyVisuals();
        }

}