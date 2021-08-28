using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour
{
    private Pathfinder _pathfinder;
    private Vector2 _targetPosition;

    [SerializeField] private float _targetSearchRange;
    [SerializeField] private int _startDamage = 1;
    [SerializeField] private float _damageZoneRadius;
    [SerializeField] private float _damageScaling = 0.5f;

    private void Awake()
    {
        _pathfinder = GetComponent<Pathfinder>();
    }

    private void FindTarget()
    {

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player != null)
        {
            float distance = Vector2.Distance(transform.position, collision.ClosestPoint(transform.position));
            int damage = (int)(((_damageZoneRadius - distance) * _damageScaling + 1) * _startDamage);
            player.Damage(damage);
        }
    }
}
