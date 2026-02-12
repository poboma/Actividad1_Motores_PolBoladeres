using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CursorDisparador : MonoBehaviour
{
    // Para asignar la cámara
    [SerializeField] Camera laCamaraQueQuieroUtilizar;

    // Se guarda la cámara principal
    Camera mainCamera;

    private void Start()
    {
        // Guarda en mainCamera la cámara MainCamera del proyecto
        mainCamera = Camera.main;
    }

    RaycastHit hit;
    void Update()
    {
        // Comprueba si el botón izquierdo está siendo presionado
        if (Mouse.current.leftButton.isPressed)
        {
            // Crea un rayo que sale desde mainCamera y apunta a la posición del ratón
            Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            // Comprueba si el rayo impacta
            if (Physics.Raycast(ray, out hit, 5f))
            {
                // Comprueba si el objeto golpeado tiene la etiqueta Boton
                if (hit.collider.CompareTag("Boton"))
                {
                    // Busca el script Boton en el objeto golpeado
                    // Llama al método Pulsar()
                    hit.collider.GetComponent<EnemyAI>().EnemigoDisparado();
                }
            }
            //Debug.Log(Mouse.current.position.ReadValue());
            // Muestra el rayo en la escena (no en el juego)
            //Debug.DrawRay(ray.origin, ray.direction * 1f, Color.red, 0.1f);
        }
    }
}