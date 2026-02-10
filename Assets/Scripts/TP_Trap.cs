using UnityEngine;

public class FakePathTrigger : MonoBehaviour
{
    public Transform respawnUbi;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = Object.FindFirstObjectByType < GameManager >();
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
          
            gameManager.EnterTPTrap(respawnUbi);
            gameObject.SetActive(false);
        }
    }
}


