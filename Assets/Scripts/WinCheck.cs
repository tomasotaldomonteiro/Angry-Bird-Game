using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class WinCheck : MonoBehaviour
{
    private bool triggered;

    void Update()
    {
        if (triggered) return;

        if (GameObject.FindGameObjectsWithTag("Building").Length == 0)
        {
            triggered = true;
            StartCoroutine(WinSequence());
        }
    }

    IEnumerator WinSequence()
    {
        yield return new WaitForSeconds(1f);

        GameUI.Instance?.ShowWin();

        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
