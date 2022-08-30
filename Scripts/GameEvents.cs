using UnityEngine;
using System;

public class GameEvents : MonoBehaviour
{
    public static GameEvents Current { get; private set; }

    private void Awake()
    {
        Current = this;
    }

    public delegate void TowerEvents(int argument);
    public event TowerEvents onChangeMoney;

    public void onChangeMoneyInvoke(int argument)
    {
        onChangeMoney?.Invoke(argument);
    }

    
    // public delegate void EnemyEvents(Vector3 argument);
    // public event EnemyEvents onEnemyDie;

    // public void onEnemyDieInvoke(Vector3 argument)
    // {
    //     onEnemyDie?.Invoke(argument);
    // }
}
