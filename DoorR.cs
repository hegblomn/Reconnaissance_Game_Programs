using UnityEngine;

public class DoorR : MonoBehaviour
{
    public float moveDistance = 1; 
    public float moveSpeed = 2; 
    private Vector3 initialPosition;
    private Vector3 rightDirection;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        initialPosition = transform.position;
        rightDirection = transform.forward;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, initialPosition + (rightDirection * moveDistance), Time.deltaTime * moveSpeed);
    }
}

