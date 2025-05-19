using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
using System;

public class CollectableAndActivate : MonoBehaviour
{
    #region Variaveis
    [System.Serializable]
    public class PlatformData
    {
        public GameObject platformObject;
        public Collider platformCollider;
        public Renderer platformRenderer;
        public Color originalColor;
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
    [SerializeField] float redPlatformDestroyRange = 1f;
    [SerializeField] float greenPlatformDestroyRange = 10f;
    [SerializeField] LayerMask redLayer;
    [SerializeField] LayerMask greenLayer;
    bool active;

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
        if (Input.GetKeyDown(KeyCode.R) && hasColor[ColorType.Red])
        {
            Collider[] platformsInRange = Physics.OverlapSphere(this.transform.position, redPlatformDestroyRange, redLayer);

            foreach (Collider plat in platformsInRange)
            {
                //Debug.Log(plat.transform.position);
                if (plat.GetComponent<RedBlock>() != null)
                {
                    plat.GetComponent<RedBlock>().DeactivateBlock();
                }
                //Destroy(plat.gameObject);
            }
            //TogglePlatforms(ColorType.Red);
        }

        if (Input.GetKeyDown(KeyCode.G) && hasColor[ColorType.Green])
        {
            Collider[] platformsInRange = Physics.OverlapSphere(this.transform.position, greenPlatformDestroyRange, greenLayer);
            
            foreach (Collider plat in platformsInRange)
            {
                plat.isTrigger = !plat.isTrigger;
                active = plat.isTrigger ? true : false;
                Color newColor = plat.GetComponent<Renderer>().material.color;                               
                newColor.a = active ? inactiveAlpha : activeAlpha;
                plat.GetComponent<Renderer>().material.color = newColor;
            }
            //TogglePlatforms(ColorType.Green);
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

                
                //platform.platformCollider.enabled = false;
                platform.platformCollider.isTrigger = true;
                Color newColor = platform.originalColor;
                newColor.a = false ? activeAlpha : inactiveAlpha;
                platform.platformRenderer.material.color = newColor;
            }
        }
    }

    private void SetPlatformState(PlatformData platform, bool active)
    {        
        /*
        if( Vector3.Distance(this.transform.position, platform.platformObject.transform.position) < greenPlatformDestroyRange)
        {
            platform.platformCollider.enabled = active;

            Color newColor = platform.originalColor;
            newColor.a = active ? activeAlpha : inactiveAlpha;
            platform.platformRenderer.material.color = newColor;
        }  */
        
        //platform.platformCollider.enabled = active;

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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, redPlatformDestroyRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(this.transform.position, greenPlatformDestroyRange);
    }
}