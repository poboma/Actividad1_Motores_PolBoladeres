using UnityEngine;

// Código que hace que las trampas devuelvan al jugador al incio al tocarlo
public class Trampa_Destroy : MonoBehaviour
{

    private void OnTriggerStay(Collider other)
    {
        // Comprueba si los objetos dentro del trigger tienen la etiqueta Player
        if (other.CompareTag("Player"))
        {
            void startOver();

            Debug.Log("Jugador eliminado");
        }
    }
}
