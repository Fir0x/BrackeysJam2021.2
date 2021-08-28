using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : CharacterBaseClass, IHero
{
    public override void Attack(Vector2 direction)
    {
        Debug.Log(name + " has performed attack");
    }
}
