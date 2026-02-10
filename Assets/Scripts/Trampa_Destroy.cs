using UnityEngine;

// Código que hace que las trampas devuelvan al jugador al incio al tocarlo
public class Trampa_Destroy : MonoBehaviour
{

    public MovimientoCapsula movimientoCapsula;
    private void OnTriggerStay(Collider other)
    {
        // Comprueba si los objetos dentro del trigger tienen la etiqueta Player
        if (other.CompareTag("Player"))
        {
            movimientoCapsula.startOver();

            Debug.Log("Jugador eliminado");
        }
    }
}
