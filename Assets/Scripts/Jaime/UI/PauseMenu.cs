using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenuUi;
    public static bool gameIsPaused = false;

    private void Start()
    {
        Btn_Resume();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Btn_Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    void Pause()
    {
        pauseMenuUi.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }
    public void Btn_Resume()
    {
        pauseMenuUi.SetActive(false);
        Time.timeScale = 1.0f;
        gameIsPaused = false;
    }
    public void Btn_menu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("MainMenu");
    }
    public void Btn_exit()
    {
        Application.Quit();
    }
}
