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
        GameUI.Instance?.SetBirds(totalBirds);
    }

    public void UseBird()
    {
        totalBirds--;

        Debug.Log("Bird used: " + totalBirds);

        GameUI.Instance?.SetBirds(totalBirds);

        if (totalBirds <= 0)
        {
            GameUI.Instance?.ShowLose();
        }
    }

    void UpdateUI()
    {
        GameUI.Instance?.SetBirds(totalBirds);
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