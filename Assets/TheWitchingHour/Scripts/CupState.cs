using UnityEngine;
using TMPro;
using System.Collections.Generic;

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

    public List<ToppingType> toppings = new List<ToppingType>();
    public int max_toppings = 2;

    public float locked_until;
    public bool locked => Time.time < locked_until;

    private void Start()
    {
        ApplyVisuals();
    }

    public bool AddTopping(ToppingType t){
        if (locked || contents == CupContents.Empty || toppings.Count >= max_toppings) return false;

        toppings.Add(t);
        ApplyVisuals();
        return true;
    }

    public void ApplyVisuals()
    {
        bool hasBase = contents != CupContents.Empty;

        world_liquid.gameObject.SetActive(hasBase);
        held_liquid.gameObject.SetActive(hasBase);

        string toppings_text = toppings.Count == 0 ? "None": string.Join(", ", toppings);

        string label = hasBase ? "Base: " + contents + "\nToppings: " + toppings_text: "Base: None\nToppings: " + toppings_text;
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

    public void ResetCup()
    {
        contents = CupContents.Empty;
        toppings.Clear();
        ApplyVisuals();
    }

    public void LockFor(float seconds)
    {
        locked_until = Time.time + seconds;
    }
}