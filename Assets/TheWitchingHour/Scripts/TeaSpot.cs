using UnityEngine;

public class TeaSpot : MonoBehaviour
{

    public void OnCupPlaced(CupState cup)
        {
            if (cup.contents != CupContents.Empty) return;
            Debug.Log("has tea");
            cup.contents = CupContents.Tea;
            cup.ApplyVisuals();
        }

}