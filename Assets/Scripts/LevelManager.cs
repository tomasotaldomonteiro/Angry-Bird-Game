using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    public int totalBirds = 3;

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
            StartCoroutine(LoseDelay());
        }
    }

    IEnumerator LoseDelay()
    {
        yield return new WaitForSeconds(0.6f);

        if (GameObject.FindGameObjectsWithTag("Building").Length > 0)
        {
            GameUI.Instance?.ShowLose();
        }
    }
}