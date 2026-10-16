using System.Collections;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    public Customer customer_prefab;
    public Transform spawn_point;
    public float spawn_delay = 2f;
    public int completed;

    private void Start()
    {
        SpawnCustomer();
    }

    public void SpawnCustomer()
    {
        Customer cust = Instantiate(customer_prefab, spawn_point.position, spawn_point.rotation);
        cust.spawner = this;
    }

    public void CustomerCompleted(Customer cust)
    {
        completed +=1;
        Destroy(cust.gameObject);
        StartCoroutine(SpawnAfterDelay());
    }

    public void CustomerExpired(Customer cust)
    {
        Destroy(cust.gameObject);
        StartCoroutine(SpawnAfterDelay());
    }

    private IEnumerator SpawnAfterDelay()
    {
        yield return new WaitForSeconds(spawn_delay);
        SpawnCustomer();
    }
}