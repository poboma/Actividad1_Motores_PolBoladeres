using UnityEngine;

public class GoalTrigger : MonoBehaviour
{
    public GameObject ControlsUI;
    public Transform ubicacionFinal;
    public Vector3 direccionFinal;
    public GameObject textoRestart;
    public GameObject ubicacionInicial;
    public GameObject particulas;

    private CharacterController jugador;
    private MonoBehaviour MovimientoCapsula;
    private bool llegaMeta = false;

    private void Start()
    {
        jugador = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>();
        MovimientoCapsula = jugador.GetComponent<MonoBehaviour>();
        textoRestart.SetActive(false);
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
            if (jugador != null)
            {
                jugador.enabled = false;
                jugador.transform.position = ubicacionFinal.position;
                jugador.transform.forward = direccionFinal.normalized;
                jugador.enabled = true;
                if (MovimientoCapsula != null)
                    MovimientoCapsula.enabled = false;
            }

            ControlsUI.SetActive(false);  
            textoRestart.SetActive(true);
            particulas.SetActive(true);
            llegaMeta = true;
        }
    }
    private void ReiniciarLaberinto()
    {
        if (jugador != null)
        {
            jugador.enabled = false;
            jugador.transform.position = ubicacionInicial.transform.position;
            jugador.transform.forward = Vector3.forward*-1; 
            jugador.enabled = true;
            if (MovimientoCapsula != null)
                MovimientoCapsula.enabled = true;
        }

        ControlsUI.SetActive(true);   
        textoRestart.SetActive(false);
        particulas.SetActive(false);    
        llegaMeta = false;
    }
}
