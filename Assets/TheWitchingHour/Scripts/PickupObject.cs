// followed the following tutorial: "How To Pick Up an Item - Unity" by OgunZZ on Youtube

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupObject : MonoBehaviour
{
    public GameObject BlockOnPlayer;
    public CupState cup;
    private bool player_in_range;
    private bool picked_up;
    public CarryTopping carrier;
    public LayerMask cup_mask;
    public float max_distance = 3f;
    public Camera cam;
    public SoundEffects pickup_sound;

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
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        if (player_in_range && !cup.locked && !carrier.holding && Input.GetMouseButtonDown(0) && Physics.Raycast(ray, out RaycastHit hit, max_distance, cup_mask, QueryTriggerInteraction.Collide))
        {
            pickup_sound.PlayAudio(3f, 0.2f);
            BlockOnPlayer.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}