using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
    public static GameUI Instance;

    public TMP_Text birdText;
    public GameObject endPanel;
    public TMP_Text resultText;

    public static bool gameEnded;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        UpdateBirds(3);
    }

    public void UpdateBirds(int count)
    {
        Debug.Log("UI UPDATE: " + count);

        if (birdText != null)
        {
            birdText.text = "Birds: " + count;
        }
        else
        {
            Debug.LogError("BirdText is NOT assigned in Inspector!");
        }
    }

    public void ShowWin()
    {
        gameEnded = true;
        endPanel.SetActive(true);
        resultText.text = "YOU WIN!";
    }

    public void ShowLose()
    {
        gameEnded = true;
        endPanel.SetActive(true);
        resultText.text = "YOU LOSE!";
    }

    public void Restart()
    {
        gameEnded = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}