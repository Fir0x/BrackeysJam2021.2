using System;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Vector2 _direction;
    [SerializeField] private float _speed;
    private int _damage;
    [SerializeField] private LayerMask _collisionMask;
    private Type _shooterType;

    public void SetupProjectile(Vector2 direction, int damage, Type shooterType)
    {
        _direction = direction;
        _damage = damage;
        _shooterType = shooterType;
    }

    private void Update()
    {
        if (_direction != Vector2.zero)
            transform.Translate(_direction * _speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CharacterBaseClass character = collision.GetComponent<CharacterBaseClass>();
        if (character != null)
        {
            if (character.GetType() != _shooterType)
                character.Damage(_damage);
        }
    }
}
