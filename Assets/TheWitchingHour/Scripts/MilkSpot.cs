using System.Collections;
using UnityEngine;
using PW; // namespace of the CoffeeShopStarterPack asset

public class MilkSpot : MonoBehaviour
{
    public CooldownVisual cooldown_visual;
    public float seconds = 1.5f; // milk fastest
    public SoundEffects sound;
    public BewerageMaker maker;

    public void OnCupPlaced(CupState cup)
    {
        if (cup.contents != CupContents.Empty) return;
        StartCoroutine(Pour(cup));
    }

    private IEnumerator Pour(CupState cup)
    {
        cup.LockFor(seconds);
        cooldown_visual.ShowVisual(seconds);
        
        maker.SendMessage("OnMouseUp");

        // sending message to activate the pour animation
        yield return new WaitForSeconds(1f);

        sound.PlayAudio(seconds);

        yield return new WaitForSeconds(1f);

        Debug.Log("milk");
        cup.contents = CupContents.Milk;
        maker.UnSetTheCup(); // need to unset cup for animation to work for next time
        cup.ApplyVisuals();
    }

}