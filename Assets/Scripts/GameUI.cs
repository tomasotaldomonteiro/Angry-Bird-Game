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

    public void SetBirds(int count)
    {
        Debug.Log("UI birds: " + count);
        birdText.text = "Birds: " + count;
    }

    public void ShowWin()
    {
        gameEnded = true;
        endPanel.SetActive(true);
        resultText.text = "YOU WIN!";
    }

    public void ShowLose()
    {
        Debug.Log("SHOW LOSE CALLED");

        gameEnded = true;
        endPanel.SetActive(true);
        resultText.text = "YOU LOSE!";
    }

    public void Restart()
    {
        Debug.Log("RESTART CLICKED");
        gameEnded = false;
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name
        );
    }
}