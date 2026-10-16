using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class SinkSpot : MonoBehaviour
{
    public CupState cupState;
    public CooldownVisual cooldown_visual;
    public float seconds = 1.5f;
    public SoundEffects sound;

    public void OnCupPlaced(CupState cup)
    {
        cup.LockFor(seconds);
        cooldown_visual.ShowVisual(seconds);
        sound.PlayAudio(6f, 1.5f);

        Debug.Log("washed");
        cupState.ResetCup();
    }
}