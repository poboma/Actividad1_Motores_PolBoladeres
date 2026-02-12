using System.Collections;
using UnityEngine;
using UnityEngine.Audio;


public class Vida_Jugador : MonoBehaviour
{
    public int vidaMaxima = 4;
    public int vidaActual;
    public MovimientoCapsula movimientoCapsula;
    public GameObject[] vidas;
    //public GameObject Vida2;
    //public GameObject Vida3;
    //public GameObject Vida4;
    public AudioSource muerteAudio;
    private bool PuedeRecibirDaño = true;
    public float CooldownDaño = 2.0f;
    public AudioSource[] recibirDaño;


        void Start()
    {
        vidaActual = vidaMaxima;
        ActualizarUI();
    }

    public void CambiarVida(int cantidad)
    {
        if (!PuedeRecibirDaño)
        {
            return;
        }

        int vidaAnterior = vidaActual;

        vidaActual += cantidad;
        vidaActual = Mathf.Clamp(vidaActual, 0, vidaMaxima);

        Debug.Log("Vida actual: " + vidaActual);

        if (cantidad < 0 && vidaActual > 0 && recibirDaño.Length > 0)
        {
            int randomIndex = Random.Range(0, recibirDaño.Length);
            recibirDaño[randomIndex].Play();
        }
        ActualizarUI();

        //if (vidaActual == vidaMaxima)
        //{
        //    return;
        //}

        //if (vidaActual == 3)
        //{
        //    Vida4.SetActive(false);
        //}

        //if (vidaActual == 2)
        //{
        //    Vida3.SetActive(false);
        //}

        //if (vidaActual == 1)
        //{
        //    Vida2.SetActive(false);
        //}
       
        if (vidaActual <= 0)
        {
            Morir();
        }

        else
        {
            StartCoroutine(Invencibilidad());
        }


    }
    void ActualizarUI()
    {
        for (int i = 0; i < vidas.Length; i++)
        {
            vidas[i].SetActive(i < vidaActual);
        }
        

    }
    IEnumerator Invencibilidad()
    {
        PuedeRecibirDaño = false;
        yield return new WaitForSeconds(CooldownDaño);
        PuedeRecibirDaño = true;
    }

    public void Morir()
    {
        muerteAudio.Play();
        movimientoCapsula.startOver();
        EnemyAI[] enemigos = FindObjectsByType<EnemyAI>(FindObjectsSortMode.None);
        foreach (EnemyAI enemigo in enemigos)
        {
            enemigo.ResetToPatrol();
            GameManager.instance.RespawnEnemigos();
            
        }
        vidaActual = vidaMaxima;
        //Vida1.SetActive(true);
        //Vida2.SetActive(true);
        //Vida3.SetActive(true);
        //Vida4.SetActive(true);
        ActualizarUI();
        Debug.Log("Jugador muerto");
    }
}