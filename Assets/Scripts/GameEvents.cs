using System;
using UnityEngine;

public static class GameEvents
{
    // События для переключения управления на дрон и обратно на игрока
    public static event Action OnSwitchToDrone;
    public static event Action OnSwitchToPlayer;

    // Вызов события переключения на дрон
    public static void SwitchToDrone()
    {
        OnSwitchToDrone?.Invoke();
    }

    // Вызов события переключения на игрока
    public static void SwitchToPlayer()
    {
        OnSwitchToPlayer?.Invoke();
    }
}