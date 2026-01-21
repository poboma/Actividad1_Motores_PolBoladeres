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
    [Header("AUDIO")] 
    [SerializeField] private AudioSource llavesAudio;
    [SerializeField] private AudioSource puertaAudio;


    private ColorKey detectedKey;
    private void Update()
    {
        textoKey.gameObject.SetActive(false);
        textoDoor.gameObject.SetActive(false);
        detectedKey = null;
        //detectedCanvas.gameObject.SetActive(false);
        if (Physics.Raycast(transform.position, transform.forward * -1, out RaycastHit hit, 1.5f, layerMask))
        {
            if (hit.collider.CompareTag("KeyTrigger"))
            {
                detectedKey = hit.collider.GetComponent<ColorKey>();
                if (detectedKey != null)
                {

                    textoKey.gameObject.SetActive(true);
                }
            }
            if (hit.collider.TryGetComponent<DoorTrigger>(out var doorTrigger))
            {
                textoDoor.gameObject.SetActive(true);
            }
        }
        //Collider[] colliders = Physics.OverlapSphere(transform.position, radius, layerMask);

        //for (int i = 0; i < colliders.Length; i++)
        //{
        //    if (colliders[i].CompareTag("KeyTrigger"))
        //    {
        //        Debug.Log("Hay trigger");
        //        anyKeyTriggerDetected = true;
        //    }
        //}

    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        if (detectedKey != null)
        {
            GameObject[] doors = GameObject.FindGameObjectsWithTag(detectedKey.doorTag);
            foreach (GameObject door in doors)
            {
                door.SetActive(false);
            }
                       
            detectedKey.gameObject.SetActive(false);
            StartCoroutine(PlayDoorSequence());
        }
    }

    private IEnumerator PlayDoorSequence()
    {
        llavesAudio.Play();

       yield return new WaitForSeconds(llavesAudio.clip.length);

        puertaAudio.Play();
    }
}

       
    

