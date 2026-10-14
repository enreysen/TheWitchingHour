// followed the following tutorial: "How To Pick Up an Item - Unity" by OgunZZ on Youtube

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupObject : MonoBehaviour
{
    public GameObject BlockOnPlayer;
    private bool player_in_range;
    private bool picked_up;
    public CarryTopping carrier;

    void Start()
    {
        picked_up = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            player_in_range = true;
        };
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player_in_range = false;
        }
    }

    private void Update()
    {
       if (player_in_range && !carrier.holding && Input.GetMouseButtonDown(1))
        {
            BlockOnPlayer.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}