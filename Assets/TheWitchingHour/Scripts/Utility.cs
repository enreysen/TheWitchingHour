using System;

public static class Utility
{

    public static T GetRandomCustomerType<T>() where T : Enum
    {
        Array values = Enum.GetValues(typeof(T));
        int index = UnityEngine.Random.Range(0, values.Length);

        return (T)values.GetValue(index);
    }

    public static T GetRandomValue<T>(bool includeFirst = false, int length = 11) where T : Enum
    {
        Array values = Enum.GetValues(typeof(T));
        int start = includeFirst ? 0 : 1;
        int index = UnityEngine.Random.Range(start, length);

        // milk and coffee equal chance (40%), tea less chance (20%)
        if (index <= 4){
            index = 1;
        }
        else if (index > 4 && index <= 8){
            index = 2;
        }
        else {
            index = 3;
        }
            
        return (T)values.GetValue(index);
    }

    public static int GetRandomNumber(int lower, int upper)
    {
        Random random = new Random();
        return random.Next(lower, upper);
    }
}