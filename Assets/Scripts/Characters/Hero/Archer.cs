using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : CharacterBaseClass, IHero
{
    [SerializeField] private GameObject projectilePrefab;
    public override void Attack(Vector2 direction)
    {
        GameObject instance = Instantiate(projectilePrefab, (Vector2)transform.position + 0.3f * direction, Quaternion.identity);
        Projectile arrow = instance.GetComponent<Projectile>();
        arrow.SetupProjectile(direction, _attack, true);
    }
}
