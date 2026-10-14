using UnityEngine;

public class PlayerPush : MonoBehaviour
{
    public bool pushUnlocked = false;
    public float pushForce = 5f;

    private Rigidbody targetRb;
    private bool inRange = false;

    void Update()
    {
        if (!pushUnlocked)
            return;
        if (!inRange || targetRb == null)
            return;

        
        if (Input.GetMouseButton(0))
            PushObject();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Push"))
        {
            pushUnlocked = true;
            Destroy(other.gameObject);
            return;
        }

        if (!pushUnlocked)
            return;
        if (!other.CompareTag("Heavy"))
            return;

        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb == null)
            return;

        targetRb = rb;
        targetRb.constraints = RigidbodyConstraints.FreezeRotation;
        inRange = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Heavy"))
            return;

        inRange = false;
        targetRb = null;
    }

    void PushObject()
    {
        float horizontalMove = Input.GetAxis("Horizontal");
        float verticalMove = Input.GetAxis("Vertical");

        Vector3 direction = transform.right * horizontalMove + transform.forward * verticalMove;
        if (direction.magnitude < 0.1f)
            return;

        targetRb.AddForce(direction.normalized * pushForce, ForceMode.Force);
    }
}