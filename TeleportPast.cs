using UnityEngine;
using UnityEngine.SceneManagement;
public class TeleportPast : MonoBehaviour
{
    public string sceneName;

    private Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           LoadSceneByName(sceneName, 1f); 
        }
    
    }

    private void LoadSceneByName(string sceneName, float delay)
    {
        Invoke("LoadScene", delay);
    }

    private void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
