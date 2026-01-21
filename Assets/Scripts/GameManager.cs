using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
   public static GameManager instance;

    [Header("UI")]
    public GameObject controlsUI;
    public GameObject restartUI;

    [Header("MINIMAPA")]
    public MapCamera mapCamera;
    private bool mapVisible = true;

    [Header("JUGADOR")]
    public MovimientoCapsula movimientoCapsula;
    public CharacterController characterController;

    [Header("EFECTOS")]
    public GameObject particulas;

    private void Awake()
    {
        if(instance == null)instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        EnterGameplay();
    }

    public void EnterGameplay()
    {
        controlsUI.SetActive(true);
        restartUI.SetActive(false);
        
        movimientoCapsula.enabled = true;
        movimientoCapsula.canLook = true;

        particulas.SetActive(false);

        mapCamera.ShowMap(false);
        mapVisible = true;


        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false; 
    }

    public void EnterGoal()
    {
        controlsUI.SetActive(false);
        restartUI.SetActive(true);

        movimientoCapsula.enabled = false;
        movimientoCapsula.canLook = false;

        particulas.SetActive(true);

        mapCamera.ShowMap(false);


        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void OnToggleMap(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        mapVisible = !mapVisible;
        mapCamera.ShowMap(mapVisible);
    }
}
