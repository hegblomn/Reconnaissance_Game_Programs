using UnityEngine;

public class RuneKeyBehavior : MonoBehaviour
{
    public Transform player;
    private Animator animator;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!player)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.position) < 5f)
        {
            animator.SetBool("KeyOn", false);
        }
        else
        {
            animator.SetBool("KeyOn", true);
        }
    }
}
