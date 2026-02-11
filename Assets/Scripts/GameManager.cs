using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("UI")]
    public GameObject controlsUI;
    public GameObject restartUI;
    public GameObject trapTPUI;
    public GameObject trapKeyUI;
    public GameObject goodFruitUI;
    public GameObject trapFruitUI;
    public GameObject puntero;
    public GameObject CanvasVida;

    [Header("PUNTUACIÓN")]
    public ScoreManager scoreManager;


    [Header("MINIMAPA")]
    public MapCamera mapCamera;
    private bool mapVisible = true;

    [Header("JUGADOR")]
    public Transform jugador;
    public MovimientoCapsula movimientoCapsula;
    public CharacterController characterController;
    public Transform ubicacionInicial;

    [Header("EFECTOS")]
    public GameObject particulas;

    [Header("TRAMPA TELETRANSPORTE")]
    public float spinDuration = 1.5f;
    public float spinSpeed = 720f;
    public float FreezeTime = 1f;


    [Header("LLAVE TRAMPA")]
    public GameObject fakeKeyCanvas;
    public float speedMultiplier = 0.3f;
    public float trapDuration = 25f;


    [Header("FRUITS")]
    public float pepperDuration = 15f;
    public float pepperMultiplier = 3f;
    public float bananaDuration = 25f;

    [Header("SONIDOS")]
    public AudioSource fruitAudio;
    public AudioSource windAudio;
    public AudioSource fakeKeyAudio;
    public AudioSource llavesAudio;
    public AudioSource puertaAudio;
    public AudioSource EndAudio;
    public AudioSource ambientMusic;


    public List<GameObject> enemigos = new List<GameObject>();

    private GameState currentState;

    public enum GameState
    {
        Gameplay,
        Goal,
        Trap
    }

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        EnterGameplay();
    }

    #region GAMEPLAY/GOAL
    public void EnterGameplay()
    {
        currentState = GameState.Gameplay;
        transform.position = ubicacionInicial.position;
        transform.rotation = ubicacionInicial.rotation;

        controlsUI.SetActive(true);
        trapTPUI.SetActive(false);
        trapKeyUI.SetActive(false);
        restartUI.SetActive(false);
        fakeKeyCanvas.SetActive(false);
        trapFruitUI.SetActive(false);
        goodFruitUI.SetActive(false);
        puntero.SetActive(true);

        movimientoCapsula.enabled = true;
        movimientoCapsula.canLook = true;
        movimientoCapsula.ResetSpeed();

        particulas.SetActive(false);

        mapCamera.ShowMap(false);
        mapVisible = true;
        ambientMusic.Play();


        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void EnterGoal()
    {
        currentState = GameState.Goal;

        controlsUI.SetActive(false);
        restartUI.SetActive(true);
        trapTPUI.SetActive(false);
        trapKeyUI.SetActive(false);
        fakeKeyCanvas.SetActive(false);
        trapFruitUI.SetActive(false);
        goodFruitUI.SetActive(false);
        puntero.SetActive(false);
        EndAudio.Play();


        movimientoCapsula.enabled = false;
        movimientoCapsula.canLook = false;

        particulas.SetActive(true);
        mapCamera.ShowMap(false);


        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    #endregion


    #region TRAMPAS
    public void EnterTPTrap(Transform respawnUbi)
    {
        scoreManager.RemovePoints(5);
        if (currentState != GameState.Gameplay) return;
        currentState = GameState.Trap;

        trapTPUI.SetActive(true);

        movimientoCapsula.enabled = false;
        movimientoCapsula.canLook = false;

        StartCoroutine(TPTrapSequence(respawnUbi));
    }

    private IEnumerator TPTrapSequence(Transform respawnUbi)
    {

        windAudio.Play();
        yield return new WaitForSeconds(FreezeTime);

        characterController.enabled = false;
        jugador.position = respawnUbi.position;
        characterController.enabled = true;

        float elapsed = 0f;
        while (elapsed < spinDuration)
        {
            float rotation = spinSpeed * Time.deltaTime;
            jugador.Rotate(Vector3.up * rotation);

            elapsed += Time.deltaTime;
            yield return null;
        }

        ExitTrap();
    }

    private void ExitTrap()
    {
        trapTPUI.SetActive(false);
        trapKeyUI.SetActive(false);
        fakeKeyCanvas.SetActive(false);
        trapFruitUI.SetActive(false);
        goodFruitUI.SetActive(false);


        movimientoCapsula.enabled = true;
        movimientoCapsula.canLook = true;
        movimientoCapsula.ResetSpeed();

        currentState = GameState.Gameplay;
    }
    #endregion

    #region MINIMAPA
    public void OnToggleMap(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        if (currentState != GameState.Gameplay) return;
        mapVisible = !mapVisible;
        mapCamera.ShowMap(mapVisible);
        puntero.SetActive(!mapVisible);

    }
    #endregion

    #region LLAVE FALSA
    public void TriggerFakeKeyTrap()
    {
        scoreManager.RemovePoints(10);
        if (currentState != GameState.Gameplay) return;
        currentState = GameState.Trap;


        trapKeyUI.SetActive(true);
        fakeKeyCanvas.SetActive(true);

        fakeKeyAudio.Play();
        llavesAudio.Play();


        StartCoroutine(FakeKeySequence());
    }

    private IEnumerator FakeKeySequence()
    {

        float originalSpeed = movimientoCapsula.currentSpeed;
        movimientoCapsula.SetSpeed(originalSpeed * speedMultiplier);

        yield return new WaitForSeconds(trapDuration);

        ExitTrap();
    }
    #endregion

    #region LLAVE VERDADERA

    public void OpenDoorWithKey(string doorTag, GameObject keyObject)
    {
        scoreManager.AddPoints(10);

        keyObject.SetActive(false);

        GameObject[] doors = GameObject.FindGameObjectsWithTag(doorTag);
        foreach (var door in doors)
            door.SetActive(false);

        StartCoroutine(PlayDoorSequence());
    }

    private IEnumerator PlayDoorSequence()
    {
        llavesAudio.Play();

        yield return new WaitForSeconds(llavesAudio.clip.length);

        puertaAudio.Play();
    }
    #endregion

    #region FRUITS
    public void TriggerTrapFruit()
    {
        scoreManager.RemovePoints(5);
        if (currentState != GameState.Gameplay) return;
        currentState = GameState.Trap;

        trapFruitUI.SetActive(true);
        fruitAudio.Play();

        StartCoroutine(bananaEffect());

    }

    private IEnumerator bananaEffect()
    {
        movimientoCapsula.invertControls = true;
        movimientoCapsula.invertedMouse = true;

        yield return new WaitForSeconds(bananaDuration);

        movimientoCapsula.invertControls = false;
        movimientoCapsula.invertedMouse = false;
        trapFruitUI.SetActive(false);
    }


    public void TriggerGoodFruit()
    {
        goodFruitUI.SetActive(true);
        fruitAudio.Play();

        scoreManager.AddPoints(5);

        StartCoroutine(pepperEffect());

    }

    private IEnumerator pepperEffect()
    {
        float originalSpeed = movimientoCapsula.currentSpeed;
        movimientoCapsula.SetSpeed(originalSpeed * pepperMultiplier);

        yield return new WaitForSeconds(pepperDuration);

        movimientoCapsula.SetSpeed(originalSpeed);
        goodFruitUI.SetActive(false);

    }




    #endregion
    public void RespawnEnemigos()
    {
        foreach (GameObject enemigo in enemigos)
        {
            if (enemigo != null)
                enemigo.SetActive(true);
        }
    }


}



