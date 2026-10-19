using UnityEngine;
using UnityEngine.UI;

// time remaining to complete the order
public class CountdownBar : MonoBehaviour
{
    public Image progress_bar;
    public float max_time;
    public Customer customer;
    public float Fraction => Mathf.Clamp01(remaining / max_time);
    private float remaining;
    public SoundEffects zombie_switch;
    public float sound_time;
    private bool zombie_restart = false;
    public bool paused;

    void Start()
    {
        max_time = customer.customer_type != CustomerType.Zombie ? 30f : 60f;
        //Debug.Log("Customer Type: " + customer.customer_type + " Max Time: " + max_time);
        ResetTimer();
    }

    void Update()
    {
        if (paused) return;
        
        remaining -= Time.deltaTime;
        progress_bar.color = Color.Lerp(Color.red, Color.green, progress_bar.fillAmount);
        progress_bar.fillAmount = Mathf.Clamp01(remaining / max_time);

        if (remaining <= 0f)
        {
            customer.OrderDrink();
            customer.spawner.CustomerLeave(customer, false); // expired customer
            zombie_restart = false;
        }
        // zombies will change their mind with 1/3 time remaining
        else if (remaining <= max_time / 3 && customer.customer_type == CustomerType.Zombie && !zombie_restart){
            customer.OrderDrink(false);
            zombie_switch.PlayAudio(3);
            zombie_restart = true;
        }
    }

    public void ResetTimer()
    {
        float multiplier = customer.customer_type == CustomerType.Zombie ? 2f : 1f; // zombies are more patient
        max_time = max_time * multiplier;
        remaining = max_time;
    }
}