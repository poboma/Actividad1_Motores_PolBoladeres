using UnityEngine;
using UnityEngine.InputSystem;
public class GoalTrigger : MonoBehaviour
{
    //public GameObject ControlsUI;
    public Transform ubicacionFinal;
    public Vector3 direccionFinal;
    //public GameObject textoRestart;
    public GameObject ubicacionInicial;
    public GameObject particulas;

    private CharacterController jugador;
    //private MovimientoCapsula movimiento;
    private bool llegaMeta = false;

    private void Start()
    {
        jugador = GameObject.FindGameObjectWithTag("Player")
                            .GetComponent<CharacterController>();

    }

    private void Update()
    {

        if (llegaMeta && Input.GetKeyDown(KeyCode.R))
        {
            ReiniciarLaberinto();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!llegaMeta && other.CompareTag("Player"))
        {
                
            jugador.enabled = false;
            jugador.transform.position = ubicacionFinal.position;
            jugador.transform.forward = direccionFinal.normalized*-1;
            jugador.enabled = true;

            particulas.SetActive(true);
            GameManager.instance.EnterGoal();

                llegaMeta = true;
           
        }
    }
    private void ReiniciarLaberinto()
    {
        
        jugador.enabled = false;
        jugador.transform.position = ubicacionInicial.transform.position;
        jugador.transform.forward = Vector3.forward; 
        jugador.enabled = true;

        
        particulas.SetActive(false);   
        GameManager.instance.EnterGameplay();
        llegaMeta = false;
    }


}
