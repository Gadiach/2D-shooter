using System;
using UnityEngine;

public static class GameEvents
{
    // ������� ��� ������������ ���������� �� ���� � ������� �� ������
    public static event Action OnSwitchToDrone;
    public static event Action OnSwitchToPlayer;

    // ����� ������� ������������ �� ����
    public static void SwitchToDrone()
    {
        OnSwitchToDrone?.Invoke();
    }

    // ����� ������� ������������ �� ������
    public static void SwitchToPlayer()
    {
        OnSwitchToPlayer?.Invoke();
    }
}