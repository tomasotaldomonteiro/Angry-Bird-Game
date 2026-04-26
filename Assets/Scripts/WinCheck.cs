using UnityEngine;

public class WinCheck : MonoBehaviour
{
    private bool gameStarted = false;

    private void Start()
    {
        Invoke("EnableWinCheck", 1f); // wait 1 second
    }

    void EnableWinCheck()
    {
        gameStarted = true;
    }

    void Update()
    {
        if (!gameStarted) return;

        if (GameObject.FindGameObjectsWithTag("Building").Length == 0)
        {
            Debug.Log("YOU WIN");
            GameUI.Instance?.ShowWin();
        }
    }
}
