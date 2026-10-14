using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerPickups inventory = other.GetComponent<PlayerPickups>();
            if (inventory != null)
            {
                inventory.hasKey = true;
                Destroy(gameObject);
            }
        }
    }
}
