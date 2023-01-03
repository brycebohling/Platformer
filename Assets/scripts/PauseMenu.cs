using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private static bool isPaused = false;
    [SerializeField] GameObject pauseMenu;

    // 
    // 
    // ONE BUG WHEN JUMPING WHEN PAUSED IT JUMPS AFTER PAUSE ENDS
    // 
    // 
    private void Update() 
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                pause();
                isPaused = true;
            } else
            {
                resume();
                isPaused = false;
            }
        }
    }

    public void pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void mainMenu()
    {
        Time.timeScale = 1f;
        isPaused = false;
        SceneManager.LoadScene("MainMenu");
        
    }
}
