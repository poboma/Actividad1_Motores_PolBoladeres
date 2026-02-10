using UnityEngine;

public class Boton : MonoBehaviour
{
    [SerializeField] public GameObject boton;
    public ScoreManager scoreManager;

    // Ejecuta el metodo Pulsar() cuando es llamado en en script CursorDisparador
    // Activa las particulas asignadas y llama al metodo AbrirPuerta()
    public void Pulsar()
    {
        scoreManager.AddPoints(20);
        //Debug.Log("Boton se activa");
        //particulas.Play();
        //puerta.AbrirPuerta();
        Destroy(boton);

    }
}
