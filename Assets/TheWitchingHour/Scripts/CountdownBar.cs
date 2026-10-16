using UnityEngine;
using UnityEngine.UI;

// time remaining to complete the order
public class CountdownBar : MonoBehaviour
{
    public Image progress_bar;
    public float max_time = 30f;
    public Customer customer;
    public float Fraction => Mathf.Clamp01(remaining / max_time);

    private float remaining;

    void Start()
    {
        ResetTimer();
    }

    void Update()
    {
        remaining -= Time.deltaTime;
        progress_bar.color = Color.Lerp(Color.red, Color.green, progress_bar.fillAmount);
        progress_bar.fillAmount = Mathf.Clamp01(remaining / max_time);

        if (remaining <= 0f)
        {
            customer.OrderDrink();
            customer.spawner.CustomerLeave(customer, false); // expired customer
        }
    }

    public void ResetTimer()
    {
        remaining = max_time;
    }
}