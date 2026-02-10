using UnityEngine;

// Código que hace que las trampas devuelvan al jugador al incio al tocarlo
public class Trampa_Destroy : MonoBehaviour
<<<<<<< Updated upstream
{

=======
    
{
    public MovimientoCapsula movimientoCapsula;
>>>>>>> Stashed changes
    private void OnTriggerStay(Collider other)
    {
        // Comprueba si los objetos dentro del trigger tienen la etiqueta Player
        if (other.CompareTag("Player"))
        {
<<<<<<< Updated upstream
            void startOver();
=======
            movimientoCapsula.startOver();
>>>>>>> Stashed changes

            Debug.Log("Jugador eliminado");
        }
    }
}
