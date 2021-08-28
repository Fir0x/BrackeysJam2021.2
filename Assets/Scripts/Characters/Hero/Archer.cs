using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Archer : CharacterBaseClass, IHero
{
    [SerializeField] private GameObject _arrowPrefab;
    public override void Attack(Vector2 direction)
    {
        GameObject instance = Instantiate(_arrowPrefab, (Vector2)transform.position + 0.3f * direction, Quaternion.identity);
        Projectile arrow = instance.GetComponent<Projectile>();
        arrow.SetupProjectile(direction, _attack, this.GetType());
    }
}
