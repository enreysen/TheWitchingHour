using System;

public static class Utility
{
    public static T GetRandomValue<T>(bool includeFirst = false) where T : Enum
    {
        Array values = Enum.GetValues(typeof(T));
        int start = includeFirst ? 0 : 1;
        int index = UnityEngine.Random.Range(start, values.Length);
        return (T)values.GetValue(index);
    }

    public static int GetRandomNumber(int lower, int upper)
    {
        Random random = new Random();
        return random.Next(lower, upper);
    }
}