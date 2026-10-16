using UnityEngine;

public class SoundEffects : MonoBehaviour 
{
    public AudioSource src;
    public AudioClip sfx;

    public void PlayAudio(float stop_time, float start_time = 0){
        src.clip = sfx;
        src.time = start_time;
        src.Play();

        double stopTime = AudioSettings.dspTime + stop_time;
        src.SetScheduledEndTime(stopTime);
    }
}