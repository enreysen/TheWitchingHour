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

    // hide in inspector because spawner assigns it
    [HideInInspector] public int slot;
    [HideInInspector] public CustomerSpawner spawner;
    
    private void Start()
    {
        OrderDrink();
    }

    public void OrderDrink()
    {
        order_base = Utility.GetRandomValue<CupContents>();

        ToppingType t1 = Utility.GetRandomValue<ToppingType>(true);
        ToppingType t2 = ToppingType.None;

        Debug.Log("customer orders completed: " + spawner.completed);
        if (spawner.completed >= 3){  // after completing 3 customers, there is a chance of 2 toppings and always at least 1 topping
            while (t1 == ToppingType.Ice || t1 == ToppingType.None){
                t1 = Utility.GetRandomValue<ToppingType>(true);
            }

            while ((t2 == t1 || t2 == ToppingType.Ice) && t1 != ToppingType.None)
            {
                t2 = Utility.GetRandomValue<ToppingType>(true);
            }
        } 

        else { // start level off easy with max 1 topping
            while (t1 == ToppingType.Ice){
                t1 = Utility.GetRandomValue<ToppingType>(true);
            }
        }

        order_toppings.Clear();
        if (t1 != ToppingType.None) order_toppings.Add(t1);
        if (t2 != ToppingType.None) order_toppings.Add(t2);

        order_iced = Utility.GetRandomNumber(0, 2) == 0 ? false : true;

        string topping_text = order_toppings.Count == 0 ? "None" : string.Join(", ", order_toppings);
        order_text.text = "Base: " + order_base + (order_iced ? " (Iced)" : "") + "\nToppings: " + topping_text;

        if (countdown != null) countdown.ResetTimer();
    }

    public int FulfillOrder(CupState cup)
    {
        int tips = 0;

        if (cup.contents == order_base) tips += 4;

        if (cup.iced == order_iced) tips += 2;

        if (order_toppings.Count > 0)
        {
            int per_topping = 4 / order_toppings.Count;
            foreach (ToppingType t in order_toppings)
                if (cup.toppings.Contains(t)) tips += per_topping;
        }
        else
        {
            if (cup.toppings.Count == 0) tips += 4; // no toppings to add 4 tip to level out to 10 total
        }

        // less than half of the time left --> half the tips
        int tips_time = 0;
        if (countdown.Fraction < 0.50) tips_time = Mathf.RoundToInt(tips / 2);

        Debug.Log("Total Tip from customer: " + tips_time);
        spawner.CustomerLeave(this, true, tips); // completed order
        return tips_time;
    }
}