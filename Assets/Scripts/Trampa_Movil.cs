using UnityEngine;

// Código para trampas que se desplazan de una dirección a otra en un mismop eje
public class Trampa_Movil : MonoBehaviour
{
    public float distancia = 5f;
    public float velocidad = 2f;
<<<<<<< Updated upstream
    public Vector3 direccion = Vector3.right;
=======
    public Vector3 direccion = Vector3.left;
>>>>>>> Stashed changes

    private Vector3 posicionInicial;
    private Rigidbody rb;

    void Start()
    {
        // Guarda la posición inicial del objeto
        posicionInicial = transform.position;
        // Obtiene el componente RigidBody
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Crea un movimiento de ida y vuelta continuo
        float desplazamiento = Mathf.PingPong(Time.time * velocidad, distancia);
        // Calcula la nueva posición para desplazar el RigidBody del objeto
        Vector3 nuevaPos = posicionInicial + direccion.normalized * desplazamiento;
        // Mueve el objeto usando RigidBody
        rb.MovePosition(nuevaPos);
    }
}
