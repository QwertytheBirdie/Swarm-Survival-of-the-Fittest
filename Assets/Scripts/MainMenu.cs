using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Called when the Endless Survival button is clicked
    public void PlayEndlessSurvival()
    {
        Debug.Log("Endless Survival button clicked!");
        // Make sure this matches your scene name exactly
        SceneManager.LoadScene("EndlessSurvival");
    }

    // Called when the 2 Player Battle button is clicked
    public void PlayTwoPlayerBattle()
    {
        Debug.Log("2 Player Battle button clicked!");
        // Make sure this matches your scene name exactly
        SceneManager.LoadScene("TwoPlayerBattle");
    }

    // Called when the Quit button is clicked
    public void QuitGame()
    {
        Debug.Log("Quit Game button clicked!");
        Application.Quit(); // Will close the build
#if UNITY_EDITOR
        // Stops play mode in the Editor
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
