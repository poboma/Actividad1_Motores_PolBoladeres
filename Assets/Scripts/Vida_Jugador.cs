using System.Collections;
using UnityEngine;


public class Vida_Jugador : MonoBehaviour
{
    public int vidaMaxima = 4;
    public int vidaActual;
    public MovimientoCapsula movimientoCapsula;
    public GameObject Vida1;
    public GameObject Vida2;
    public GameObject Vida3;
    public GameObject Vida4;

    private bool PuedeRecibirDaño = true;
    public float CooldownDaño = 2.0f;

    void Start()
    {
        vidaActual = vidaMaxima;
        Vida1.SetActive(true);
        Vida2.SetActive(true);
        Vida3.SetActive(true);
        Vida4.SetActive(true);
    }

    public void CambiarVida(int cantidad)
    {
        if (!PuedeRecibirDaño)
        {
            return;
        }

        
        vidaActual += cantidad;
        vidaActual = Mathf.Clamp(vidaActual, 0, vidaMaxima);

        Debug.Log("Vida actual: " + vidaActual);

        if (vidaActual == vidaMaxima)
        {
            return;
        }

        if (vidaActual == 3)
        {
            Vida4.SetActive(false);
        }

        if (vidaActual == 2)
        {
            Vida3.SetActive(false);
        }

        if (vidaActual == 1)
        {
            Vida2.SetActive(false);
        }

        if (vidaActual <= 0)
        {
            Morir();
        }

        else
        {
            StartCoroutine(Invencibilidad());
        }


    }

    IEnumerator Invencibilidad()
    {
        PuedeRecibirDaño = false;
        yield return new WaitForSeconds(CooldownDaño);
        PuedeRecibirDaño = true;
    }

    void Morir()
    {
        movimientoCapsula.startOver();
        vidaActual = vidaMaxima;
        Vida1.SetActive(true);
        Vida2.SetActive(true);
        Vida3.SetActive(true);
        Vida4.SetActive(true);
        Debug.Log("Jugador muerto");
    }
}