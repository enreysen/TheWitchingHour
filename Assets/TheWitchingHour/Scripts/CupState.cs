using UnityEngine;
using TMPro;

public class CupState : MonoBehaviour
{
    public CupContents contents = CupContents.Empty;
    public GameObject world_cup_visual;
    public GameObject held_cup_visual;

    public TextMeshPro world_cup_label;
    public TextMeshPro held_cup_label;


    public void Update()
    {
        if (contents ==  CupContents.Coffee) 
        {
            bool hasCoffee = true;
            world_cup_visual.SetActive(hasCoffee);
            held_cup_visual.SetActive(hasCoffee);

            // no toppings for now
            string label = "Base: " + contents.ToString() + "\nToppings: None";

            world_cup_label.text = label;
            held_cup_label.text = label;
        }
        
    }
}