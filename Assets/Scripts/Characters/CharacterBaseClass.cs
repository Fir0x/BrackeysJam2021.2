using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterBaseClass : MonoBehaviour
{
    [SerializeField] protected Animator _animator;

    [SerializeField] protected int _startHealth;
    protected int _health;
    [SerializeField] protected int _moveSpeed;

    [SerializeField] protected int _attack;
    [SerializeField] protected int _attackSpeed;
    [Tooltip("Range of attack")]
    [SerializeField] protected int _range;

    [Tooltip("How long is the character stunned?")]
    [SerializeField] protected int _stunTime;

    public int Health { get => _health; }

    protected Action<Vector2> onMoveHandler;

    private void Awake()
    {
        _health = _startHealth;
    }

    public void SubscribeOnMoveHandler(Action<Vector2> handler)
    {
        onMoveHandler = handler;
    }

    public abstract void Attack(Vector2 direction);

    public virtual void Damage(int amount)
    {
        _health -= amount;
    }

    public void Heal(int amount)
    {
        _health = (_health + amount) % _startHealth;
    }

    public void Stun()
    {
        StartCoroutine(PerformStun());
    }

    private IEnumerator PerformStun()
    {
        _animator.SetBool("stun", true);
        yield return new WaitForSeconds(_stunTime);
        _animator.SetBool("stun", false);
    }
}
