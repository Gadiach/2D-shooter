using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private Camera sideCamera;
    [SerializeField] private Camera topCamera;

    private void Start()
    {
        // Подписываемся на события переключения камер
        GameEvents.OnSwitchToDrone += SwitchToTopCamera;
        GameEvents.OnSwitchToPlayer += SwitchToSideCamera;

        // Изначально активируем боковую камеру
        topCamera.gameObject.SetActive(false);
        sideCamera.gameObject.SetActive(true);
    }

    private void OnDestroy()
    {
        // Отписываемся от событий
        GameEvents.OnSwitchToDrone -= SwitchToTopCamera;
        GameEvents.OnSwitchToPlayer -= SwitchToSideCamera;
    }

    private void SwitchToTopCamera()
    {
        topCamera.gameObject.SetActive(true);
        sideCamera.gameObject.SetActive(false);
    }

    private void SwitchToSideCamera()
    {
        topCamera.gameObject.SetActive(false);
        sideCamera.gameObject.SetActive(true);
    }
}