using UnityEngine;
using System.Collections.Generic;

public class CollectableAndActivate : MonoBehaviour
{
    #region Variaveis
    [System.Serializable]
    public class PlatformData
    {
        public GameObject platformObject;
        public Collider platformCollider;
        public Renderer platformRenderer;
        [HideInInspector] public Color originalColor;
        public bool startsActive = false;
    }

    private enum ColorType { Red, Green, Blue }

    private Dictionary<ColorType, bool> hasColor = new Dictionary<ColorType, bool>
    {
        { ColorType.Red, false },
        { ColorType.Green, false },
        { ColorType.Blue, false }
    };

    [Header("Platform Settings")]
    [SerializeField] private float inactiveAlpha = 0.3f;
    [SerializeField] private float activeAlpha = 1f;

    [Header("Platform References")]
    public PlatformData[] redPlatforms;
    public PlatformData[] greenPlatforms;
    public PlatformData[] bluePlatforms;

    private Dictionary<ColorType, PlatformData[]> platformsByColor;

    #endregion

    #region Start/Update
    void Start()
    {
        platformsByColor = new Dictionary<ColorType, PlatformData[]>
        {
            { ColorType.Red, redPlatforms },
            { ColorType.Green, greenPlatforms },
            { ColorType.Blue, bluePlatforms }
        };

        InitializePlatforms(redPlatforms);
        InitializePlatforms(greenPlatforms);
        InitializePlatforms(bluePlatforms);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            TogglePlatforms(ColorType.Red);
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            TogglePlatforms(ColorType.Green);
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            TogglePlatforms(ColorType.Blue);
        }
    }
    #endregion

    #region Funções Plataforma Ativa/Desativa
    private void InitializePlatforms(PlatformData[] platforms)
    {
        foreach (PlatformData platform in platforms)
        {
            if (platform.platformObject != null)
            {
                platform.originalColor = platform.platformRenderer.material.color;
                SetPlatformState(platform, platform.startsActive);
            }
        }
    }

    private void SetPlatformState(PlatformData platform, bool active)
    {
        platform.platformCollider.enabled = active;

        Color newColor = platform.originalColor;
        newColor.a = active ? activeAlpha : inactiveAlpha;
        platform.platformRenderer.material.color = newColor;
    }

    private void TogglePlatforms(ColorType colorType)
    {
        if (hasColor[colorType])
        {
            Debug.Log($"Alternando plataformas {colorType}");
            ToggleEachPlatformIndividually(platformsByColor[colorType]);
        }
    }

    private void ToggleEachPlatformIndividually(PlatformData[] platforms)
    {
        foreach (PlatformData platform in platforms)
        {
            if (platform.platformObject != null)
            {
                SetPlatformState(platform, !platform.platformCollider.enabled);
            }
        }
    }
    #endregion

    #region Função Coletáveis RGB
    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "RedCollectable":
                CollectColor(ColorType.Red, other);
                break;

            case "GreenCollectable":
                CollectColor(ColorType.Green, other);
                break;

            case "BlueCollectable":
                CollectColor(ColorType.Blue, other);
                break;
        }
    }

    private void CollectColor(ColorType colorType, Collider other)
    {
        hasColor[colorType] = true;
        Destroy(other.gameObject);
        Debug.Log($"Coletou {colorType}! Pressione {colorType.ToString()[0]} para alternar plataformas {colorType}");
    }
    #endregion
}