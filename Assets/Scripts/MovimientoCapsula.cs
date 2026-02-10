using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;


public class MovimientoCapsula : MonoBehaviour
{
    [Header("AUDIO")]
    [SerializeField] private AudioSource pasosAudio;
    [SerializeField] private float stepThreshold = 0.1f;
    [Header("MOVIMIENTO JUGADOR")]
    [SerializeField] public float movementSpeed = 5f;
    //[SerializeField] public float turnSpeed = 90f;
    [Header("CONTROL DE CAMARA")]
    [SerializeField] public float mouseSensitivity = 50f;
    [SerializeField] public Transform cameraHolder;
    [SerializeField] public bool canLook = true;

    [Header("VELOCIDAD DINÁMICA")]
    public float currentSpeed; 
    public float defaultSpeed;

    [Header("ANIMACIONES")]
    public Animator anim;

    [Header("VARIABLES")]

    public bool invertControls = false;
    public bool invertedMouse = false;
    Vector2 rawMove = Vector2.zero;
    private float xRotation = 0f;

    public Transform ubicacionInicial;
    private CharacterController jugador;
    

    private void Awake()
    {
        jugador = GetComponent<CharacterController>();
        defaultSpeed = movementSpeed;
        currentSpeed = defaultSpeed;
    }
    private void Start()
    {

       transform.position = ubicacionInicial.position;
       transform.rotation = ubicacionInicial.rotation;
        anim = GetComponentInChildren<Animator>();
    }
    void Update()
    {
        Vector3 cameraForward = cameraHolder.forward;
        cameraForward.y = 0;
        cameraForward.Normalize();

        Vector3 cameraRight = cameraHolder.right;
        cameraRight.y = 0;
        cameraRight.Normalize();
        Vector3 movement = (cameraForward * rawMove.y + cameraRight * rawMove.x);

        if (invertControls)
            movement = -movement; 

        movement *= currentSpeed * -1;
        jugador.Move(movement * Time.deltaTime);
        if (movement.magnitude > stepThreshold )
        {
            if (!pasosAudio.isPlaying)
                pasosAudio.Play();
            anim.SetBool("isWalking", true);
            
        }
        else
        {
            pasosAudio.Stop();
            anim.SetBool("isWalking", false);
        }

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

        float invertFactor = invertedMouse ? -1f : 1f;

        float mouseX = lookDelta.x * mouseSensitivity * Time.deltaTime * invertFactor;
        float mouseY = lookDelta.y * mouseSensitivity * Time.deltaTime * -1f * invertFactor;

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

    public void SetSpeed(float movementSpeed)
    {
        currentSpeed = movementSpeed;
    }

    public void ResetSpeed()
    {
        currentSpeed = defaultSpeed;
    }
}

