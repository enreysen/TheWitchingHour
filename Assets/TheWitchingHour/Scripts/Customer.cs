using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using TMPro;

public class Customer : MonoBehaviour
{
    public TextMeshPro order_text;
    private CupContents order_base;
    private List<ToppingType> order_toppings = new List<ToppingType>();
    public CountdownBar countdown;
    public bool order_iced;
    public GameObject customer_visual;
    public float delay_next = 3f;
    
    private void Start()
    {
        OrderDrink();
    }

    public void OrderDrink()
    {
        order_base = Utility.GetRandomValue<CupContents>();

        ToppingType t1 = Utility.GetRandomValue<ToppingType>(true);
        ToppingType t2 = Utility.GetRandomValue<ToppingType>(true);

        while (t1 == ToppingType.Ice){
            t1 = Utility.GetRandomValue<ToppingType>(true);
        }

        while ((t2 == t1 || t2 == ToppingType.Ice) && t1 != ToppingType.None)
        {
            t2 = Utility.GetRandomValue<ToppingType>(true);
        }

        order_toppings.Clear();
        if (t1 != ToppingType.None) order_toppings.Add(t1);
        if (t2 != ToppingType.None) order_toppings.Add(t2);

        order_iced = Utility.GetRandomNumber(0, 2) == 0 ? false : true;

        string topping_text = order_toppings.Count == 0 ? "None" : string.Join(", ", order_toppings);
        order_text.text = "Base: " + order_base + (order_iced ? " (Iced)" : "") + "\nToppings: " + topping_text;

        if (countdown != null) countdown.ResetTimer();
    }

    private IEnumerator NextCustomer()
    {
        customer_visual.SetActive(false);
        yield return new WaitForSeconds(delay_next);

        customer_visual.SetActive(true);
        OrderDrink();
    }

    public int FulfillOrder(CupState cup)
    {
        int points = 0;

        if (cup.contents == order_base) points += 25;

        if (cup.iced == order_iced) points += 25;

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

        StartCoroutine(NextCustomer());
        return points;
    }
}