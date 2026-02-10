using UnityEngine;

public class Enemigo_RestaVida : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Vida_Jugador jugadorVida = other.GetComponent<Vida_Jugador>();
            if (jugadorVida != null)
            {
                jugadorVida.CambiarVida(-1);
            }
        }
    }
}
