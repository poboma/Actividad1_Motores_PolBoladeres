using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class FruitTriggerDetector : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] LayerMask layerMaskFruit;
    [SerializeField] GameObject textoFruit;

    private trapFruit detectedTrapFruit;
    private goodFruit detectedGoodFruit;

    private void Update()
    {
        textoFruit.gameObject.SetActive(false);
        detectedTrapFruit = null;
        detectedGoodFruit = null;


        if (Physics.Raycast(transform.position, transform.forward * -1, out RaycastHit hit, 1.5f, layerMaskFruit))
        {
            detectedTrapFruit = hit.collider.GetComponent<trapFruit>();
            if (detectedTrapFruit == null)
                detectedGoodFruit = hit.collider.GetComponent<goodFruit>();

            if (detectedTrapFruit != null || detectedGoodFruit != null)
                textoFruit.gameObject.SetActive(true);
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

       
        if (detectedTrapFruit != null)
        {
            trapFruit trapFruitToActivate = detectedTrapFruit;
            detectedTrapFruit = null;
            trapFruitToActivate.ActivateTrapFruit(GameManager.instance);
            return;
        }

        if (detectedGoodFruit != null)
        {
            goodFruit goodFruitToActivate = detectedGoodFruit;
            detectedGoodFruit = null;
            goodFruitToActivate.ActivateGoodFruit(GameManager.instance);
            return;
        }
    }


}
