using UnityEngine;

public class CarryTopping : MonoBehaviour {
    public Camera cam;
    public GameObject held_cup;
    public LayerMask topping_mask;
    public LayerMask cup_mask;
    public float max_distance = 3f;
    public GameObject[] held_visuals;

    private ToppingType held = ToppingType.None;
    public bool holding
    {
        get { return held != ToppingType.None; }
    }

    private void Update()
    {
        if (held_cup.activeSelf) return; 

        Ray ray = new Ray(cam.transform.position, cam.transform.forward);

        if (!holding)
        {
            if (Input.GetMouseButtonDown(0) && Physics.Raycast(ray, out RaycastHit hit, max_distance, topping_mask, QueryTriggerInteraction.Collide))
            {

                ToppingSource src = hit.collider.GetComponentInParent<ToppingSource>();
                Debug.Log(src); 
                if (src != null) SetHeld(src.type);
            }
        }
        else
        { 
            if (Input.GetMouseButtonDown(0) && Physics.Raycast(ray, out RaycastHit hit, max_distance, cup_mask, QueryTriggerInteraction.Collide))
            {
                CupState cup = hit.collider.GetComponentInParent<CupState>();
                if (cup != null && cup.AddTopping(held)) SetHeld(ToppingType.None);
            }
        }
    }

    private void SetHeld(ToppingType t)
    {
        for (int i = 0; i < held_visuals.Length; i++){
            if (held_visuals[i] != null){
                held_visuals[i].SetActive(i == (int)t);
                Debug.Log("slot " + i + " = " + held_visuals[i].name + " active=" + (i == (int)t), held_visuals[i]);
            } 
        }
    }
}