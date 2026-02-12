using UnityEngine;

// Código que hace que las trampas devuelvan al jugador al incio al tocarlo
public class Trampa_Destroy : MonoBehaviour
{
    public Vida_Jugador vidajugador;
    
    private void OnTriggerStay(Collider other)
    {
        // Comprueba si los objetos dentro del trigger tienen la etiqueta Player
        if (other.CompareTag("Player"))
        {
            vidajugador.Morir();

            Debug.Log("Jugador eliminado");
        }
    }
}
