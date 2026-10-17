using System.Collections;
using UnityEngine;
using PW; // namespace of the CoffeeShopStarterPack asset

public class CoffeeSpot : MonoBehaviour
{
    public CooldownVisual cooldown_visual;
    public float seconds = 3f;
    public SoundEffects sound;

    public BewerageMaker maker;

    public void OnCupPlaced(CupState cup)
    {
        if (cup.contents != CupContents.Empty) return;
        StartCoroutine(Brew(cup));
    }

    private IEnumerator Brew(CupState cup)
    {
        cup.LockFor(seconds);
        cooldown_visual.ShowVisual(seconds);

        // sending message to activate the coffee machine animation from the asset pack
        maker.SendMessage("OnMouseUp");
        
        // coffee machine audio
        sound.PlayAudio(seconds, 1f);

        yield return new WaitForSeconds(seconds);

        Debug.Log("coffee");
        cup.contents = CupContents.Coffee;
        cup.ApplyVisuals();
    }
}