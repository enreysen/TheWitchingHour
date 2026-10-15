using UnityEngine;
using TMPro;

public class CustomerInteract : MonoBehaviour
{
    public Camera cam;
    public GameObject heldCup;
    public CupState cupState;
    public LayerMask customerMask;
    public float maxDistance = 3f;
    public TextMeshProUGUI score_text;
    public float score;

    private void Update()
    {
        if (!heldCup.activeSelf || cupState.contents == CupContents.Empty || !Input.GetMouseButtonDown(0)) return;

        Ray ray = new Ray(cam.transform.position, cam.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance, customerMask, QueryTriggerInteraction.Collide))
        {
            Customer customer = hit.collider.GetComponentInParent<Customer>();
            if (customer != null)
            {
                score += customer.FulfillOrder(cupState);
                score_text.text = "Score: " + score.ToString();
                cupState.ResetCup();
            }
        }
    }
}