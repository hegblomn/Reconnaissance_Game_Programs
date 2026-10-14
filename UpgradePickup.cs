using UnityEngine;

public class UpgradePickup : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerPickups inventory = other.GetComponent<PlayerPickups>();
            if (inventory != null)
            {
                inventory.hasCard = true;
                Destroy(gameObject);
            }
        }
    }
}
