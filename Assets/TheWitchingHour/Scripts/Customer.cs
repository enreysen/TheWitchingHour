using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Customer : MonoBehaviour
{
    public TextMeshPro order_text;
    private CupContents order_base;
    private List<ToppingType> order_toppings = new List<ToppingType>();
    public CountdownBar countdown;

    private void Start()
    {
        OrderDrink();
    }

    public void OrderDrink()
    {
        order_base = EnumUtility.GetRandomValue<CupContents>();

        ToppingType t1 = EnumUtility.GetRandomValue<ToppingType>(true);
        ToppingType t2 = EnumUtility.GetRandomValue<ToppingType>(true);

        while (t2 == t1 && t1 != ToppingType.None)
        {
            t2 = EnumUtility.GetRandomValue<ToppingType>(true);
        }

        order_toppings.Clear();
        if (t1 != ToppingType.None) order_toppings.Add(t1);
        if (t2 != ToppingType.None) order_toppings.Add(t2);

        string topping_text = order_toppings.Count == 0 ? "None" : string.Join(", ", order_toppings);
        order_text.text = "Base: " + order_base + "\nToppings: " + topping_text;

        if (countdown != null) countdown.ResetTimer();
    }

    public int FulfillOrder(CupState cup)
    {
        int points = 0;

        if (cup.contents == order_base) points += 50;

        if (order_toppings.Count > 0)
        {
            int per_topping = 50 / order_toppings.Count;
            foreach (ToppingType t in order_toppings)
                if (cup.toppings.Contains(t)) points += per_topping;
        }
        else
        {
            if (cup.toppings.Count == 0) points += 50; // no toppings to add 50 points to level out to 100
        }

        // takes the points player earned and shrinks up to half depending time left on the countdown
        // serving faster means more points
        float time_multiplier = 0.5f + 0.5f * countdown.Fraction;
        points = Mathf.RoundToInt(points * time_multiplier);

        OrderDrink();
        return points;
    }
}