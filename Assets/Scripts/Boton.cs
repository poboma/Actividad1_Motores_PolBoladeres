using UnityEngine;

public class Boton : MonoBehaviour
{
    [SerializeField] public GameObject boton;

    // Ejecuta el metodo Pulsar() cuando es llamado en en script CursorDisparador
    // Activa las particulas asignadas y llama al metodo AbrirPuerta()
    public void Pulsar()
    {
        Debug.Log("Boton se activa");



        //particulas.Play();


        //puerta.AbrirPuerta();
        boton.SetActive(false);

    }
}
