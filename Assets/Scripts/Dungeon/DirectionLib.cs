using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DirectionLib
{
    public enum Direction
    {
        None,
        North,
        South,
        East,
        West
    };

    public static Direction GetDirectionFromVector2(Vector2 vector)
    {
        if (vector == Vector2.up)
            return Direction.North;
        else if (vector == Vector2.down)
            return Direction.South;
        else if (vector == Vector2.right)
            return Direction.East;
        else if (vector == Vector2.left)
            return Direction.West;
        else
            return Direction.None;
    }

    public static Direction ReverseDirection(Direction direction)
    {
        switch (direction)
        {
            case Direction.North:
                return Direction.South;
            case Direction.South:
                return Direction.North;
            case Direction.East:
                return Direction.West;
            case Direction.West:
                return Direction.East;
            default:
                return Direction.None;
        }
    }
}
