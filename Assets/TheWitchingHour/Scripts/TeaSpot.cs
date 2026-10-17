using System.Collections;
using UnityEngine;
using PW; // namespace of the CoffeeShopStarterPack asset

public class TeaSpot : MonoBehaviour
{
    public CooldownVisual cooldown_visual;
    public float seconds = 6f;
    public BewerageMaker maker;
    public SoundEffects boiling;
    public SoundEffects pouring;

    public void OnCupPlaced(CupState cup)
    {
        if (cup.contents != CupContents.Empty) return;
        StartCoroutine(Pour(cup));
    }

    private IEnumerator Pour(CupState cup)
    {
        cup.LockFor(seconds);
        cooldown_visual.ShowVisual(seconds);
        
        // tea sound audio
        boiling.PlayAudio(seconds / 2, 1f);

        yield return new WaitForSeconds(2);
        // sending message to activate the pour animation
        maker.SendMessage("OnMouseUp");

        yield return new WaitForSeconds(1);
        // tea sound audio
        pouring.PlayAudio(seconds / 2, 1f);
        

        yield return new WaitForSeconds(seconds / 2);
        Debug.Log("tea");
        cup.contents = CupContents.Tea;
        maker.UnSetTheCup(); // need to unset cup for animation to work for next time
        cup.ApplyVisuals();
    }
}