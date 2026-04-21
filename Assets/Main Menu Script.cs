using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
         public void PlayGame()
            {
                SceneManager.LoadScene("Level1"); // Trigger fade-out and load scene
            }
         
            public void QuitGame()
            {
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
            }
    
            public void BackToMenu()
            {
                SceneManager.LoadScene("Main Menu");
            }
    
}
