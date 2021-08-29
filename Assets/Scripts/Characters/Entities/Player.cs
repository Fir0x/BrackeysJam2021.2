using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player main { get; private set; }

    [Header("Player HP")]
    [SerializeField] private int _maxHp = 100;
    private int _hp;
    [SerializeField] private int _hpScaling = 10;

    [Header("Auto damage")]
    [SerializeField] private float _autoDamageTime = 1f;
    [SerializeField] private int _autoDamage = 2;
    private float _lastAutoDamage = 0;

    [Header("Soul points")]
    private int _soulCount = 0;
    [SerializeField] private int _blackoutCost = 20;
    [SerializeField] private int _soulIncrement = 1;
    [SerializeField] private float _soulIncrementTime = 1;
    private float _lastSoulIncrement = 0;
    private float _lastDamage = 0;

    private void Awake()
    {
        main = this;
        ResetHp();
    }

    private void Start()
    {
        HealthManager.main.SetMaxHealth(_maxHp);
        SoulManager.main.UpdateUI(_soulCount);
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

    public void Damage(int amount, bool autoDamage = false)
    {
        if (amount < 0)
            return;

        if (autoDamage)
            _lastAutoDamage = Time.time;
        else
            _lastDamage = Time.time;

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

    public void PayBlackout()
    {
        if (_soulCount >= _blackoutCost)
        {
            _soulCount -= _blackoutCost;
            DungeonManager.main.Blackout();
        }
    }

    private void Update()
    {
        if (Time.time - _lastAutoDamage >= _autoDamageTime)
            Damage(_autoDamage, true);

        if (Time.time - _lastSoulIncrement > _soulIncrementTime && Time.time - _lastDamage > 1)
        {
            _soulCount += _soulIncrement;
            _lastSoulIncrement = Time.time;
            SoulManager.main.UpdateUI(_soulCount);
        }
    }
}
