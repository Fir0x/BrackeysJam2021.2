using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterBaseClass : MonoBehaviour
{
    [SerializeField] protected Animator _animator;

    [SerializeField] protected int _health;
    [SerializeField] protected int _moveSpeed;

    [SerializeField] protected int _attackSpeed;
    [Tooltip("Range of attack")]
    [SerializeField] protected int range;

    [Tooltip("How long is the character stunned?")]
    [SerializeField] protected int _stunTime;

    public int Health { get => _health; }

    protected Action<Vector2> onMoveHandler;

    public void SubscribeOnMoveHandler(Action<Vector2> handler)
    {
        onMoveHandler = handler;
    }

    public virtual void Attack()
    {
        Debug.Log(name + " has performed attack");
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
