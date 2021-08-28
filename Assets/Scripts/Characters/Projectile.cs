using System;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Vector2 _direction;
    [SerializeField] private float _speed;
    private int _damage;
    [SerializeField] private LayerMask _collisionMask;
    private bool _isFromHero;

    public void SetupProjectile(Vector2 direction, int damage, bool isFromHero)
    {
        _direction = direction;
        _damage = damage;
        _isFromHero = isFromHero;
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
            if ((_isFromHero && character is IMonster) || (!_isFromHero && character is IHero))
            {
                character.Damage(_damage);
                Destroy(gameObject);
            }
        }
    }
}
