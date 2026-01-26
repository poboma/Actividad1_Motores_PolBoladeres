using UnityEngine;

public class goodFruit : MonoBehaviour
{

    public void ActivateGoodFruit(GameManager gameManager)
    {
        gameManager.TriggerGoodFruit();
        gameObject.SetActive(false);
    }
}