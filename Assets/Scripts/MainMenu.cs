using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Called when the Start button is pressed
    public void PlayGame()
    {
        // Loads the next scene in Build Settings
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // Optional version (if you want to load by scene name instead)
    public void PlayGameByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // Called when the Quit button is pressed
    public void QuitGame()
    {
        Debug.Log("Quit button pressed!");

        // Works only in a built game (EXE, APP)
        Application.Quit();
    }
}
