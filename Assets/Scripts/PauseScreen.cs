using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScreen : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject quitConfirmationUI; // Assign this in the inspector

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseMenuUI.activeSelf)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f; // Resume the game
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f; // Pause the game
    }

    public void QuitGame()
    {
        // Show the confirmation dialog
        quitConfirmationUI.SetActive(true);
        pauseMenuUI.SetActive(false); // Hide the pause menu
    }

    public void ConfirmQuit()
    {
        Debug.Log("Quitting game...");
        Application.Quit(); // Quit the application
    }

    public void CancelQuit()
    {
        // Hide the confirmation dialog
        quitConfirmationUI.SetActive(false);
        pauseMenuUI.SetActive(true); // Show the pause menu
    }
}