using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using static UnityEditor.PlayerSettings.SplashScreen;

public class MovimientoCapsula : MonoBehaviour
{
    [Header("MOVIMIENTO JUGADOR")]
    [SerializeField] public float movementSpeed = 5f;
    [SerializeField] public float turnSpeed = 90f;
    [Header("CONTROL DE CAMARA")]
    [SerializeField] public float mouseSensitivity = 50f;
    [SerializeField] public Transform cameraHolder;
    [SerializeField] public bool canLook = true;
    Vector2 rawMove = Vector2.zero;
    private float xRotation = 0f;

    public Transform ubicacionInicial;
    private CharacterController jugador;
    

    private void Awake()
    {
        jugador = GetComponent<CharacterController>();
    }
    private void Start()
    {

        //transform.position = ubicacionInicial.position;
        //transform.rotation = ubicacionInicial.rotation;

    }
    void Update()
    {
        Vector3 cameraForward = cameraHolder.forward;
        cameraForward.y = 0;
        cameraForward.Normalize();

        Vector3 cameraRight = cameraHolder.right;
        cameraRight.y = 0;
        cameraRight.Normalize();
        Vector3 movement = (cameraForward * rawMove.y + cameraRight * rawMove.x) * movementSpeed*-1;

        //float turn = Input.GetAxis("Horizontal"); 
        //float travel = Input.GetAxis("Vertical");
        //if (Input.GetKeyDown(KeyCode.R))
        //{
        //    resetLaberinth();
        //}

        //transform.Rotate(0f, turn * turnSpeed * Time.deltaTime, 0f);
        //Vector3 localMovement = new Vector3(0, 0, travel * movementSpeed);
        //Vector3 worldMovement = transform.TransformDirection(localMovement);
        //jugador.Move(worldMovement * Time.deltaTime);

        jugador.Move(movement * Time.deltaTime);
    }

    private void startOver()
    {
        jugador.enabled = false;

        transform.position = ubicacionInicial.position;
        transform.rotation = ubicacionInicial.rotation;

        xRotation = 0f;
        cameraHolder.localRotation = Quaternion.identity;

        jugador.enabled = true;
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        rawMove = context.ReadValue<Vector2>();
    }
    public void OnLook(InputAction.CallbackContext context)
    {
        if (!canLook) return;
        Vector2 lookDelta = context.ReadValue<Vector2>();

        float mouseX = lookDelta.x * mouseSensitivity * Time.deltaTime;
        float mouseY = lookDelta.y * mouseSensitivity * Time.deltaTime*-1;

        
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        
        cameraHolder.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }
    public void OnReset(InputAction.CallbackContext context)
    {
        if (context.performed)
            startOver();
    }

    //public void OnInteract(InputAction.CallbackContext context)
    //{
    //    if (context.performed)

    //}
}

