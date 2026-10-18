using UnityEngine;

public class PlaceObject : MonoBehaviour
{
    public Camera camera;
    public GameObject cup_held;
    public GameObject cup_world;
    public GameObject cup_highlight;
    public LayerMask spot_mask;
    public float max_distance = 2f;
    public CupState cupState;

    public SoundEffects place_sound;
    public float sound_time = 3f;

    private void Start()
    {
        cupState = cup_world.GetComponent<CupState>();
    }
    // Update is called once per frame
    void Update()
    {
        //  return if not holding a cup
        if (!cup_held.activeSelf) {
            cup_highlight.SetActive(false);
            return;
        }; 

        Ray ray = new Ray(camera.transform.position, camera.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, max_distance, spot_mask, QueryTriggerInteraction.Collide))
        {
            Transform spot = hit.collider.transform;
            cup_highlight.transform.SetPositionAndRotation(spot.position, spot.rotation);
            cup_highlight.SetActive(true);

            if (Input.GetMouseButtonDown(0))
            {
                place_sound.PlayAudio(sound_time);

                cup_world.transform.SetPositionAndRotation(spot.position, spot.rotation);
                cup_world.SetActive(true);
                cup_held.SetActive(false);
                cup_highlight.SetActive(false);

                CoffeeSpot coffee = hit.collider.GetComponentInParent<CoffeeSpot>();
                if (coffee != null) coffee.OnCupPlaced(cupState);

                TeaSpot tea = hit.collider.GetComponentInParent<TeaSpot>();
                if (tea != null) 
                {
                    Vector3 offset = new Vector3(0.50f, 0f, -0.50f);
                    cup_world.transform.SetPositionAndRotation(spot.position + offset, spot.rotation);
                    tea.OnCupPlaced(cupState);
                }

                MilkSpot milk = hit.collider.GetComponentInParent<MilkSpot>();
                if (milk != null) 
                {
                    Vector3 offset = new Vector3(0f, 0.05f, -0.769f);
                    cup_world.transform.SetPositionAndRotation(spot.position - offset, spot.rotation);
                    milk.OnCupPlaced(cupState);
                }

                SinkSpot sink = hit.collider.GetComponentInParent<SinkSpot>();
                if (sink != null) sink.OnCupPlaced(cupState);
            } 
        }
        else 
        {
            cup_highlight.SetActive(false);
        }
    }
}
