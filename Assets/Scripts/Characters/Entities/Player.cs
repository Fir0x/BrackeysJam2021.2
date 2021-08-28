using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player main { get; private set; }

    [SerializeField] private int _maxHp = 100;
    private int _hp;
    [SerializeField] private int _hpScaling = 10;

    private void Awake()
    {
        main = this;
        ResetHp();
    }

    public void UpgradePlayer()
    {
        _maxHp += _hpScaling;
        ResetHp();
    }

    private void ResetHp()
    {
        _hp = _maxHp;
    }

    public void Damage(int amount)
    {
        if (amount < 0)
            return;

        _hp -= amount;
        if (_hp <= 0)
            Death();
    }

    private void Death()
    {
        throw new System.NotImplementedException();
    }
}
