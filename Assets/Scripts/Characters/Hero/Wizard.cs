using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Wizard : Archer
{
    [SerializeField] private int _healAmount;
    [SerializeField] private int _healZoneRadius;

    public void Heal(Vector2 position)
    {
        List<Collider2D> hits = new List<Collider2D>();
        Physics2D.OverlapCircle(position, _healZoneRadius, new ContactFilter2D(), hits);
        List<CharacterBaseClass> characterHits = hits.FindAll(hit => hit.GetComponent<CharacterBaseClass>() != null)
                                                     .ConvertAll(hit => hit.GetComponent<CharacterBaseClass>());
        foreach (CharacterBaseClass character in characterHits)
        {
            if (character is IHero)
                character.Heal(_healAmount);
        }
    }
}
