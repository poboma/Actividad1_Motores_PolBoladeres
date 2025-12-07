using UnityEngine;

public class MapCamera : MonoBehaviour
{
    public GameObject miniMapa;   
    [SerializeField] public KeyCode toggleKey; 

    void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            miniMapa.SetActive(!miniMapa.activeSelf);
        }
    }
}