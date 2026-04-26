using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
    public static GameUI Instance;

    public TMP_Text birdText;
    public GameObject endPanel;
    public TMP_Text resultText;

    private void Awake()
    {
        Instance = this;
    }

    public void SetBirds(int count)
    {
        birdText.text = "Birds: " + count;
    }

    public void ShowLose()
    {
        endPanel.SetActive(true);
        resultText.text = "YOU LOSE!";
    }

    public void ShowWin()
    {
        endPanel.SetActive(true);
        resultText.text = "YOU WIN!";
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}