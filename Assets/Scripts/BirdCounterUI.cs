using UnityEngine;
using TMPro;

public class BirdCounterUI : MonoBehaviour
{
    public static BirdCounterUI Instance;

    public TMP_Text text;

    private void Awake()
    {
        Instance = this;
    }

    public void UpdateText(int count)
    {
        Debug.Log("UI TEXT UPDATE: " + count);

        if (text != null)
        {
            text.text = "Birds: " + count;
        }
        else
        {
            Debug.LogError("TEXT NOT ASSIGNED!");
        }
    }
}
