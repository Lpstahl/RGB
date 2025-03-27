using UnityEngine;

public class CollectableAndActivate : MonoBehaviour
{
    [System.Serializable]
    public class PlatformData
    {
        public GameObject platformObject;
        public Collider platformCollider;
        public Renderer platformRenderer;
        [HideInInspector] public Color originalColor;
    }

    private bool hasRed = false;
    private bool hasGreen = false;
    private bool hasBlue = false;

    [Header("Platform Settings")]
    [SerializeField] private float inactiveAlpha = 0.3f;
    [SerializeField] private float activeAlpha = 1f;

    [Header("Platform References")]
    public PlatformData[] redPlatforms;
    public PlatformData[] greenPlatforms;
    public PlatformData[] bluePlatforms;

    void Start()
    {
        // Inicializa todas as plataformas
        InitializePlatforms(redPlatforms);
        InitializePlatforms(greenPlatforms);
        InitializePlatforms(bluePlatforms);
    }

    private void InitializePlatforms(PlatformData[] platforms)
    {
        foreach (PlatformData platform in platforms)
        {
            if (platform.platformObject != null)
            {
                // Guarda a cor original
                platform.originalColor = platform.platformRenderer.material.color;

                // Configura estado inicial (inativo)
                SetPlatformState(platform, false);
            }
        }
    }

    private void SetPlatformState(PlatformData platform, bool active)
    {
        // Ativa/desativa o collider
        platform.platformCollider.enabled = active;

        // Ajusta a transparência
        Color newColor = platform.originalColor;
        newColor.a = active ? activeAlpha : inactiveAlpha;
        platform.platformRenderer.material.color = newColor;
    }

    private void TogglePlatforms(PlatformData[] platforms)
    {
        // Inverte o estado de todas as plataformas
        foreach (PlatformData platform in platforms)
        {
            if (platform.platformObject != null)
            {
                // Inverte o estado(se tiver ativa, desativa; se tiver desativa, ativa)
                bool newState = !platform.platformCollider.enabled;
                SetPlatformState(platform, newState);
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && hasRed)
        {
            TogglePlatforms(redPlatforms);
        }

        if (Input.GetKeyDown(KeyCode.G) && hasGreen)
        {
            TogglePlatforms(greenPlatforms);
        }

        if (Input.GetKeyDown(KeyCode.B) && hasBlue)
        {
            TogglePlatforms(bluePlatforms);
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        // Verifica se o objeto que colidiu é um coletável
        switch (other.tag)
        {
            case "RedCollectable":
                hasRed = true;
                Destroy(other.gameObject);
                Debug.Log("Coletou Vermelho! Pressione R para ativar/desativar plataformas vermelhas");
                break;

            case "GreenCollectable":
                hasGreen = true;
                Destroy(other.gameObject);
                Debug.Log("Coletou Verde! Pressione E para ativar/desativar plataformas verdes");
                break;

            case "BlueCollectable":
                hasBlue = true;
                Destroy(other.gameObject);
                Debug.Log("Coletou Azul! Pressione W para ativar/desativar plataformas azuis");
                break;
        }
    }
}