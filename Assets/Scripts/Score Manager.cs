using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public int birdsUsed;
    public int buildingsDestroyed;

    private void Awake()
    {
        Instance = this;
    }

    public void AddBirdUsed()
    {
        birdsUsed++;
        Debug.Log("Bird used: " + birdsUsed);
    }

    public void AddBuildingDestroyed()
    {
        buildingsDestroyed++;
        Debug.Log("Building destroyed: " + buildingsDestroyed);
    }

    public int GetStars()
    {
        if (birdsUsed <= 1) return 3;
        if (birdsUsed <= 3) return 2;
        return 1;
    }
}