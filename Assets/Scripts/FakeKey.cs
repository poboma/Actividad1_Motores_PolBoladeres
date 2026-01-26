using UnityEngine;

public class FakeKey : MonoBehaviour
{
   
    public void ActivateTrap(GameManager gameManager)
    {
        gameManager.TriggerFakeKeyTrap();
        gameObject.SetActive(false);
    }
}