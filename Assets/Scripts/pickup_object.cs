// followed the following tutorial: "How To Pick Up an Item - Unity" by OgunZZ on Youtube

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupObject : MonoBehaviour
{
    public GameObject BlockOnPlayer;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Check if 'E' key is held down to pick up the Block
            if (Input.GetKey(KeyCode.E))
            {
                gameObject.SetActive(false);
                BlockOnPlayer.SetActive(true);
            }
        }
    }
}