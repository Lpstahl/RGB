using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] GameObject Menu;
    [SerializeField] GameObject Options;
    [SerializeField] GameObject Credits;

    private void Start()
    {
        HideUi();
        Menu.SetActive(true);
    }
    void HideUi()
    {
        Menu.SetActive(false);
        Options.SetActive(false);
        Credits.SetActive(false);
    }
    public void OpenScreen(int screen)
    {
        HideUi();
        switch (screen)
        {
            case 0:
                Menu.SetActive(true);
                break;

            case 1:
                SceneManager.LoadScene("Game");
                break;

            case 6:
                Application.Quit();
                break;

            case 7:
                Options.SetActive(true);
                break;

            case 8:
                Credits.SetActive(true);
                break;

            default:
                Debug.LogWarning("Tela não encontrada");
                break;


        }
    }
}