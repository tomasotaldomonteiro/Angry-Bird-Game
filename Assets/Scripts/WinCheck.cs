using System.Collections;
using UnityEngine;

public class WinCheck : MonoBehaviour
{
    private bool checking;

    void Update()
    {
        if (checking) return;

        if (GameObject.FindGameObjectsWithTag("Building").Length == 0)
        {
            checking = true;
            StartCoroutine(WinDelay());
        }
    }

    IEnumerator WinDelay()
    {
        yield return new WaitForSeconds(0.5f);

        if (GameObject.FindGameObjectsWithTag("Building").Length == 0)
        {
            GameUI.Instance?.ShowWin();
        }

        checking = false;
    }
}