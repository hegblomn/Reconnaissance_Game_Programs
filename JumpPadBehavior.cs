using UnityEngine;

public class JumpPadBehavior : MonoBehaviour
{
    public float jumpHeight = 0.4f;

    [Header("Active State")]
    public bool isActive = true;
    public Renderer padRenderer;
    public Material activeMaterial;
    public Material inactiveMaterial;

    [Header("SFX")]
    public AudioClip jumpPadSFX;
    [Range(0f, 1f)] public float sfxVolume = 1f;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>(); 

        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 1f;
        audioSource.volume = sfxVolume;

        ApplyMaterial();
    }

    public void SetActive(bool active)
    {
        isActive = active;
        ApplyMaterial();
    }

    void OnTriggerEnter(Collider collider)
    {
        if (!isActive)
            return;

        if (collider.CompareTag("Player"))
        {
            Debug.Log("Pad ready for boosted jump");
            PlayerController3P player3P = collider.GetComponent<PlayerController3P>();
            PlayerController player = collider.GetComponent<PlayerController>();
            if (player3P)
                player3P.JumpPowerOn(jumpHeight);
            if (player)
                player.JumpPowerOn(jumpHeight);
        }
    }

    void OnTriggerStay(Collider collider)
    {
        if (!isActive)
            return;

        if (collider.CompareTag("Player"))
        {
            PlayerController3P player3P = collider.GetComponent<PlayerController3P>();
            PlayerController player = collider.GetComponent<PlayerController>();
            if (player3P)
                player3P.JumpPowerOn(jumpHeight);
            if (player)
                player.JumpPowerOn(jumpHeight);
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            Debug.Log("Can't Jump");
            PlayerController3P player3P = collider.GetComponent<PlayerController3P>();
            PlayerController player = collider.GetComponent<PlayerController>();
            if (player3P)
                player3P.JumpPowerOff();
            if (player) 
                player.JumpPowerOff();
        }
    }

    void ApplyMaterial()
{
    if (padRenderer == null)
        return;
    if (isActive)
    {
        padRenderer.material = activeMaterial;
    }
    else
    {
        padRenderer.material = inactiveMaterial;
    }
}
}