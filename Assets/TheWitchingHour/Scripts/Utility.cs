using System;

public static class Utility
{

    public static T GetRandomCustomerType<T>(int end = 4) where T : Enum
    {
        Array values = Enum.GetValues(typeof(T));
        int index = UnityEngine.Random.Range(0, end);

        return (T)values.GetValue(index);
    }

   public static T GetRandomValue<T>(bool includeFirst = false, bool randomBase = false) where T : Enum
    {
        Array values = Enum.GetValues(typeof(T));
        int index;

        if (randomBase) // 40% chance coffee and milk, 20% tea
        {
            int roll = UnityEngine.Random.Range(0, 10);
            if (roll < 4) index = 1;
            else if (roll < 8) index = 2;
            else index = 3;
        }

        else
        {
            int start = includeFirst ? 0 : 1;
            index = UnityEngine.Random.Range(start, values.Length);
        }

        return (T)values.GetValue(index);
    }

    public static int GetRandomNumber(int lower, int upper)
    {
        Random random = new Random();
        return random.Next(lower, upper);
    }
}