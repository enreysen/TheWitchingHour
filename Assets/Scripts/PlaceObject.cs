using UnityEngine;

public class PlaceObject : MonoBehaviour
{
    public Camera camera;
    public GameObject cup_held;
    public GameObject cup_world;
    public GameObject cup_highlight;
    public LayerMask spot_mask;
    public float max_distance = 2f;

    // Update is called once per frame
    void Update()
    {
        //  return if not holding a cup
        if (!cup_held.activeSelf) {
            Debug.Log("not active");
            cup_highlight.SetActive(false);
            return;
        }; 

        Ray ray = new Ray(camera.transform.position, camera.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, max_distance, spot_mask, QueryTriggerInteraction.Collide))
        {
            Debug.Log("here");
            Transform spot = hit.collider.transform;
            cup_highlight.transform.SetPositionAndRotation(spot.position, spot.rotation);
            cup_highlight.SetActive(true);

            if (Input.GetMouseButtonDown(0))
            {
                cup_world.transform.SetPositionAndRotation(spot.position, spot.rotation);
                cup_world.SetActive(true);
                cup_held.SetActive(false);
                cup_highlight.SetActive(false);
            } 
        }
        else 
        {
            cup_highlight.SetActive(false);
        }
    }
}
