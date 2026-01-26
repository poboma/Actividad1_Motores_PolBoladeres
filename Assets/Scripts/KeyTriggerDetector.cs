using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class KeyTriggerDetector : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] LayerMask layerMask;
    [SerializeField] TextMeshProUGUI textoKey;
    [SerializeField] TextMeshProUGUI textoDoor;

    private ColorKey detectedKey;
    private FakeKey detectedFakeKey;
    private DoorTrigger detectedDoor;
    private void Update()
    {
        textoKey.gameObject.SetActive(false);
        textoDoor.gameObject.SetActive(false);

        if (Physics.Raycast(transform.position, transform.forward * -1, out RaycastHit hit, 1.5f, layerMask))
        {
            detectedFakeKey = hit.collider.GetComponent<FakeKey>();

            if (detectedFakeKey == null)
                detectedKey = hit.collider.GetComponent<ColorKey>();

            hit.collider.TryGetComponent<DoorTrigger>(out detectedDoor);

            if (detectedFakeKey != null || detectedKey != null)
                textoKey.gameObject.SetActive(true);
            if (detectedDoor != null)
                textoDoor.gameObject.SetActive(true);
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        if (detectedFakeKey != null)
        {
            FakeKey fakeKeyToActivate = detectedFakeKey;
            detectedFakeKey = null;
            fakeKeyToActivate.ActivateTrap(GameManager.instance);
            return;
        }

        if (detectedKey != null)
        {
            GameManager.instance.OpenDoorWithKey(detectedKey.doorTag, detectedKey.gameObject);
            detectedKey = null; 
        }
    }


}

       
    

