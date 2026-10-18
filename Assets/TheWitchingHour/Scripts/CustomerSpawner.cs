using System.Collections;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    public Customer customer_prefab;
    public Transform spawn_point;
    public float spawn_delay = 2f;
    public int completed;
    public bool first_slot_filled;
    public Transform[] spawn_points;
    private Customer[] occupants;

    public SoundEffects fill_order_perfect;
    public SoundEffects fill_order_partial;
    public SoundEffects fill_order_incorrect;
    private int sound_time = 3;

    private void Start()
    {
        occupants = new Customer[spawn_points.Length];
        SpawnCustomer(0);
        StartCoroutine(ExtraCustomer());
    }

    public void SpawnCustomer(int slot)
    {
        Transform point = spawn_points[slot];
        Customer cust = Instantiate(customer_prefab, point.position, point.rotation);
        cust.spawner = this;

        bool pack_mentality = WerewolfPresent();
        cust.slot = slot;
        CustomerType type = CustomerType.Werewolf;
        if (!pack_mentality) {
            type = (completed > 3) ? Utility.GetRandomCustomerType<CustomerType>() : Utility.GetRandomCustomerType<CustomerType>(2);
        }
        
        cust.SetType(type);

        occupants[slot] = cust;
    }

    public void CustomerLeave(Customer cust, bool complete, int tips = 0)
    {
        if (complete) {
            completed += 1;

            if (tips == 10) fill_order_perfect.PlayAudio(sound_time);
            else if (tips > 0) fill_order_partial.PlayAudio(sound_time, 1f);
            else fill_order_incorrect.PlayAudio(sound_time);
        } 

        occupants[cust.slot] = null;
        Destroy(cust.gameObject);
        
        if (cust.slot == 0 && !first_slot_filled){
            StartCoroutine(SpawnAfterDelay());
        }
    }

    private IEnumerator ExtraCustomer()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            bool pack_mentality = WerewolfPresent();
            int chance = pack_mentality ? 10 : 30;

            if (completed > 2) { // wait until 2 customers for chance of extra customers

                for (int i = 1; i < spawn_points.Length; i++)
                {
                    if (occupants[i] == null && Utility.GetRandomNumber(0, chance) == 1)
                        SpawnCustomer(i);
                }
            }
        }
    }

    private IEnumerator SpawnAfterDelay()
    {
        first_slot_filled = true;
        yield return new WaitForSeconds(spawn_delay);
        SpawnCustomer(0);
        first_slot_filled = false;
    }

    private bool WerewolfPresent()
    {
        foreach (Customer c in occupants)
        {
            if (c != null && c.customer_type == CustomerType.Werewolf) return true;
        }
        return false;
    }
}