using UnityEngine;

public class VentOpen : MonoBehaviour
{
    public float riseAmount = 3f;
    public float riseSpeed = 2f;
    public AudioClip ventSFX;

    private bool isActivated = false;
    private Transform[] ventTransforms;
    private Vector3[] ventTargets;

    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        GameObject[] ventObjects = GameObject.FindGameObjectsWithTag("Vent");
        ventTransforms = new Transform[ventObjects.Length];
        ventTargets = new Vector3[ventObjects.Length];

        for (int i = 0; i < ventObjects.Length; i++)
        {
            ventTransforms[i] = ventObjects[i].transform;
            ventTargets[i] = ventObjects[i].transform.position + Vector3.up * riseAmount;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isActivated = true;
            if(ventSFX)
            {
                AudioSource.PlayClipAtPoint(ventSFX, ventTransforms[0].position);
            }
            audioSource.Play();
        }
    }

    private void Update()
    {
        if (!isActivated) return;

        for (int i = 0; i < ventTransforms.Length; i++)
        {
            if (ventTransforms[i] == null) continue;
            ventTransforms[i].position = Vector3.MoveTowards(
                ventTransforms[i].position,
                ventTargets[i],
                riseSpeed * Time.deltaTime
            );
        }
    }
}