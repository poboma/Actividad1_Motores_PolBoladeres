using UnityEngine;

public class trapFruit : MonoBehaviour
{

    public void ActivateTrapFruit(GameManager gameManager)
    {
        gameManager.TriggerTrapFruit();
        gameObject.SetActive(false);
    }
}