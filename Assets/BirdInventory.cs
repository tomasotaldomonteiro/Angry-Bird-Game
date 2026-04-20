using UnityEngine;

public static class BirdInventory
{
    private const string UnlockedPrefix = "UnlockedBird_";

    public static void Unlock(BirdAbilityType birdType)
    {
        if (birdType == BirdAbilityType.None)
        {
            return;
        }

        PlayerPrefs.SetInt(UnlockedPrefix + birdType, 1);
        PlayerPrefs.Save();
    }

    public static bool IsUnlocked(BirdAbilityType birdType)
    {
        if (birdType == BirdAbilityType.None)
        {
            return true;
        }

        return PlayerPrefs.GetInt(UnlockedPrefix + birdType, 0) == 1;
    }
}

