using UnityEngine;


public class Vida_Jugador : MonoBehaviour
{
    public int vidaMaxima = 4;
    public int vidaActual;
    public MovimientoCapsula movimientoCapsula;
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
        movimientoCapsula.startOver();
        vidaActual = vidaMaxima;
        Debug.Log("Jugador muerto");
    }
}