using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player main { get; private set; }

    [SerializeField] private int _maxHp = 100;
    private int _hp;
    [SerializeField] private int _hpScaling = 10;
    [SerializeField] private float _autoDamageTime = 1f;
    [SerializeField] private int _autoDamage = 2;
    private float _lastAutoDamage = 0;

    private void Awake()
    {
        main = this;
        ResetHp();
    }

    private void Start()
    {
        HealthManager.main.SetMaxHealth(_maxHp);
    }

    public void UpgradePlayer()
    {
        _maxHp += _hpScaling;
        ResetHp();
        HealthManager.main.SetMaxHealth(_maxHp);
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
        _hp = _hp < 0 ? 0 : _hp;

        HealthManager.main.UpdateHealthBar(_hp);

        if (_hp == 0)
            Death();
    }

    private void Death()
    {
        GameManager.main.Loss();
    }

    private void Update()
    {
        if (Time.time - _lastAutoDamage >= _autoDamageTime)
        {
            _lastAutoDamage = Time.time;
            Damage(_autoDamage);
        }
    }
}
