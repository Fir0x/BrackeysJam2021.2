using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTarget : CharacterBaseClass
{
    [SerializeField] private Pathfinder pathfinder;

    public override void Attack(Vector2 direction)
    { }

    private void Start()
    {
        pathfinder.SetTarget(this);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            onMoveHandler.Invoke(transform.position);
    }
}
