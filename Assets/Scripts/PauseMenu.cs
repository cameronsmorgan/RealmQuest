using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI; // Reference to the pause menu UI panel
    public Button resumeButton; // Reference to the resume button
    public Button quitButton; // Reference to the quit button

    public bool isPaused = false;

    private void Start()
    {
        // Add listeners to button clicks
        resumeButton.onClick.AddListener(ResumeGame);
        quitButton.onClick.AddListener(QuitGame);

        // Hide pause menu initially
        TogglePauseMenu(false);
    }

    private void Update()
    {
        // Check for pause input (e.g., pressing the "P" key)
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0f; // Freeze time (pauses the game)
        isPaused = true;
        TogglePauseMenu(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f; // Unfreeze time (resumes the game)
        isPaused = false;
        TogglePauseMenu(false);
    }

    public void QuitGame()
    {
        SceneManager.LoadScene("MainMenu"); // Load the main menu scene
    }

    public void TogglePauseMenu(bool showMenu)
    {
        pauseMenuUI.SetActive(showMenu); // Show/hide the pause menu UI panel
    }
}
