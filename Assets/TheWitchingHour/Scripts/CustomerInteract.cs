using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CustomerInteract : MonoBehaviour
{
    public Camera cam;
    public GameObject heldCup;
    public CupState cupState;
    public LayerMask customerMask;
    public float maxDistance = 3f;
    public TextMeshProUGUI score_text;
    public float score = 0;

    private void Update()
    {
        if (!heldCup.activeSelf || cupState.contents == CupContents.Empty || !Input.GetMouseButtonDown(0)) return;
        if (score >= 250) SceneManager.LoadScene("Win");

        Ray ray = new Ray(cam.transform.position, cam.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance, customerMask, QueryTriggerInteraction.Collide))
        {
            Customer customer = hit.collider.GetComponentInParent<Customer>();
            if (customer != null)
            {
                score += customer.FulfillOrder(cupState);
                score_text.text = "Tips: $" + score.ToString();
                cupState.ResetCup();
                
            }
        }
    }
}