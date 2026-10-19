using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject mainBackground;
    public GameObject controls;
    public GameObject quirks;
    private bool paused = false;
    private bool second_menu = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused)
                ResumeGame();
            else
                PauseGame();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            pauseMenu.SetActive(false);
            controls.SetActive(true);
            second_menu = true;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            pauseMenu.SetActive(false);
            quirks.SetActive(true);
            second_menu = true;
        }

        if (Input.GetKeyDown(KeyCode.B) && second_menu)
        {
            pauseMenu.SetActive(true);
            quirks.SetActive(false);
            controls.SetActive(false);
            second_menu = false;
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            Application.Quit();
        }
    }

    void PauseGame()
    {
        Time.timeScale = 0f;
        paused = true;
        pauseMenu.SetActive(true);
        mainBackground.SetActive(true);

    }

    void ResumeGame()
    {
        Time.timeScale = 1f;
        paused = false;
        pauseMenu.SetActive(false);
        mainBackground.SetActive(false);
        controls.SetActive(false);
        quirks.SetActive(false);
    }
}   