using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private Camera sideCamera;
    [SerializeField] private Camera topCamera;

    private void Start()
    {
        // ������������� �� ������� ������������ �����
        GameEvents.OnSwitchToDrone += SwitchToTopCamera;
        GameEvents.OnSwitchToPlayer += SwitchToSideCamera;

        // ���������� ���������� ������� ������
        topCamera.gameObject.SetActive(false);
        sideCamera.gameObject.SetActive(true);
    }

    private void OnDestroy()
    {
        // ������������ �� �������
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