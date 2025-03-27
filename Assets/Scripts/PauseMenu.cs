using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [Header("Referências do Menu")]
    [SerializeField] private GameObject pauseMenuPanel;
    [SerializeField] private RectTransform menuTransform;
    [SerializeField] private Button firstSelectedButton;

    [Header("Configurações de Animação")]
    [SerializeField] private float scaleDuration = 0.4f;
    [SerializeField] private float showScale = 1f;
    [SerializeField] private Ease showEase = Ease.OutBack;
    [SerializeField] private Ease hideEase = Ease.InBack;
    [SerializeField] private Image backgroundFade;

    private bool isPaused = false;
    private Vector3 originalScale;

    private void Awake()
    {
        originalScale = menuTransform.localScale;
        menuTransform.localScale = Vector3.zero;
        pauseMenuPanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        if (isPaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    private void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;

        pauseMenuPanel.SetActive(true);

        backgroundFade.DOFade(0.7f, scaleDuration).SetUpdate(true);

        // Animação de aparecer
        menuTransform.DOScale(originalScale * showScale, scaleDuration)
            .SetEase(showEase)
            .SetUpdate(true);

        // Seleciona o primeiro botão automaticamente
        firstSelectedButton.Select();
    }

    private void ResumeGame()
    {
        backgroundFade.DOFade(0f, scaleDuration).SetUpdate(true);

        // Animação de desaparecer
        menuTransform.DOScale(Vector3.zero, scaleDuration)
            .SetEase(hideEase)
            .SetUpdate(true)
            .OnComplete(() => {
                pauseMenuPanel.SetActive(false);
                isPaused = false;
                Time.timeScale = 1f;
            });
    }

    // Chamado pelo botão "Continuar"
    public void OnResumePressed()
    {
        ResumeGame();
    }

    // Chamado pelo botão "Menu Principal"
    public void OnMainMenuPressed()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}