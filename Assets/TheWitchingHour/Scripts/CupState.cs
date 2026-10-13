using UnityEngine;
using TMPro;

public class CupState : MonoBehaviour
{
    public CupContents contents = CupContents.Empty;

    public GameObject world_visual;
    public GameObject held_visual;

    public Renderer world_liquid;
    public Renderer held_liquid;

    public TextMeshPro world_cup_label;
    public TextMeshPro held_cup_label;

    public Color coffee_color = new Color(0.25f, 0.15f, 0.08f, 1f);
    public Color tea_color = new Color(0.8157f, 0.9412f, 0.7529f, 1.0f);
    public Color milk_color = new Color(0.95f, 0.95f, 0.9f, 1f);

    private void Start()
    {
        ApplyVisuals();
    }

    public void ApplyVisuals()
    {
        bool hasBase = contents != CupContents.Empty;

        world_liquid.gameObject.SetActive(hasBase);
        held_liquid.gameObject.SetActive(hasBase);

        string label = hasBase ? "Base: " + contents + "\nToppings: None": "Base: None\nToppings: None";
        world_cup_label.text = label;
        held_cup_label.text = label;

        if (!hasBase) return;

        Color c = Color.white;
        switch (contents)
        {
            case CupContents.Coffee: c = coffee_color; break;
            case CupContents.Tea:    c = tea_color;    break;
            case CupContents.Milk:   c = milk_color;   break;
        }
        world_liquid.material.color = c;
        held_liquid.material.color = c;

        held_visual.SetActive(hasBase);
        world_visual.SetActive(hasBase);
    }
}