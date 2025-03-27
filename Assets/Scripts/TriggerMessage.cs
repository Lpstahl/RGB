using System.Collections;
using TMPro;
using UnityEngine;

public class TriggerMessage : MonoBehaviour
{
    public TextMeshProUGUI messageText;  // Referência ao texto na UI
    public string message = "Olá, viajante! Seja bem-vindo.";  // Mensagem a ser exibida
    public float displayTime = 3f;  // Tempo que a mensagem fica visível
    public float typingSpeed = 0.05f; // Velocidade da digitação

    private bool hasTriggered = false; // Para evitar reativação

    private void Start()
    {
        if (messageText != null)
            messageText.gameObject.SetActive(false); // Esconde o texto ao iniciar
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hasTriggered && other.CompareTag("Player"))
        {
            hasTriggered = true;  // Impede ativações repetidas
            StartCoroutine(ShowMessage());
            Debug.Log("Player entrou no trigger");
        }
    }

    IEnumerator ShowMessage()
    {
        if (messageText == null) yield break;

        messageText.gameObject.SetActive(true);
        messageText.text = "";  // Limpa o texto antes de escrever

        foreach (char letter in message)
        {
            messageText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        yield return new WaitForSeconds(displayTime);
        messageText.gameObject.SetActive(false);
        Debug.Log("Player saiu no trigger");
    }
}
