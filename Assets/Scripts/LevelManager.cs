using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [SerializeField] private int totalBirds = 3;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        UpdateUI();
    }

    public void UseBird()
    {
        totalBirds--;

        UpdateUI();

        Debug.Log("Bird used. Left: " + totalBirds);

        if (totalBirds <= 0)
        {
            LoseGame();
        }
    }

    void UpdateUI()
    {
        GameUI.Instance?.UpdateBirds(totalBirds);
    }

    void LoseGame()
    {
        Debug.Log("YOU LOSE");

        GameUI.Instance?.ShowLose();
    }

    public int GetBirds()
    {
        return totalBirds;
    }
}