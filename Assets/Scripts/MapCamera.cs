using UnityEngine;

public class MapCamera : MonoBehaviour
{
    public GameObject miniMapa;

    public void ShowMap(bool show)
    {
        miniMapa.SetActive(show);
    }
}