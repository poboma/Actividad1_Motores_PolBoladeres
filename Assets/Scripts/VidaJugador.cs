using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int vidaMaxima = 4;
    public int vidaActual;

    void Start()
    {
        vidaActual = vidaMaxima;
    }

    public void CambiarVida(int cantidad)
    {
        vidaActual += cantidad;
        vidaActual = Mathf.Clamp(vidaActual, 0, vidaMaxima);

        Debug.Log("Vida actual: " + vidaActual);

        if (vidaActual <= 0)
        {
            Morir();
        }
    }

    void Morir()
    {
        Debug.Log("Jugador muerto");
    }
}
