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

        cust.slot = slot;
        occupants[slot] = cust;
    }

    public void CustomerLeave(Customer cust, bool complete)
    {
        if (complete) completed += 1;

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


            if (completed > 2) { // wait until 2 customers for chance of extra customers

                for (int i = 1; i < spawn_points.Length; i++)
                {
                    if (occupants[i] == null && Utility.GetRandomNumber(0, 25) == 1)
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
}