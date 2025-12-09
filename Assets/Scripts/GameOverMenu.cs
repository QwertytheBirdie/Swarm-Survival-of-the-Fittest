using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    // Called when the Main Menu button on the Game Over screen is clicked
    public void GoToMainMenu()
    {
        Debug.Log("Main Menu button clicked on Game Over screen!");
        SceneManager.LoadScene("MainMenu"); // Make sure this matches your Main Menu scene name
    }

    // Optional: Restart the current scene
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
