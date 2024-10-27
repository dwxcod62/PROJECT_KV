using System.Collections.Generic;

public static class Data
{
    private static readonly Dictionary<string, int> animation_id = new Dictionary<string, int>
    {
        { "Coin", 0 },
        { "Heal", 1 }
    };

    public static int GetAniId(string key)
    {
        if (animation_id.TryGetValue(key, out int value))
        {
            return value;
        }
        return -1;
    }
}
