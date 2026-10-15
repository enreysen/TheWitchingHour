using UnityEngine;
using UnityEngine.UI;

// Shows a progress bar on a station while its cooldown runs
public class CooldownVisual : MonoBehaviour
{
    public Image progress_bar;
    public GameObject cooldown_object;

    private float duration;
    private float current;
    private bool active;

    void Start()
    {
        Hide();
    }

    void Update()
    {
        if (!active) return;

        current += Time.deltaTime;
        progress_bar.fillAmount = Mathf.Clamp01(current / duration);

        if (current >= duration)
        {
            Hide();
        }
    }

    public void ShowVisual(float seconds)
    {
        duration = seconds;
        current = 0f;
        active = true;
        progress_bar.fillAmount = 0f;
        cooldown_object.SetActive(active);
    }

    private void Hide()
    {
        active = false;
        cooldown_object.SetActive(active);
    }
}