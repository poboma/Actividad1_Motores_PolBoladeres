using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class PlayerMovementSimple : MonoBehaviour
{
    [SerializeField] public float movementSpeed = 5f;
    [SerializeField] public float turnSpeed = 90f;
    public Transform ubicacionInicial;
    private CharacterController player;
    private void Start()
    {
        player = GetComponent<CharacterController>();
    }
    void Update()
    {
        float turn = Input.GetAxis("Horizontal"); 
        float travel = Input.GetAxis("Vertical");
        if (Input.GetKeyDown(KeyCode.R))
        {
            resetLaberinth();
        }

        transform.Rotate(0f, turn * turnSpeed * Time.deltaTime, 0f);
        Vector3 localMovement = new Vector3(0, 0, travel * movementSpeed);
        Vector3 worldMovement = transform.TransformDirection(localMovement);
        player.Move(worldMovement * Time.deltaTime);
    }

    private void resetLaberinth()
    {
        player.enabled = false;
        transform.position = ubicacionInicial.position;
        transform.forward = ubicacionInicial.forward;
        player.enabled = true;

      
    }
}
