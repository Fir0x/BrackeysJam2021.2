using UnityEngine;
using UnityEngine.Tilemaps;

public class DoorAdapter : MonoBehaviour
{
    /*[SerializeField] private Tilemap walls;
    [SerializeField] private Door _northDoor;
    [SerializeField] private Door _southDoor;
    [SerializeField] private Door _eastDoor;
    [SerializeField] private Door _westDoor;

    public void SetDoor(Vector2 roomSize, DirectionLib.Direction doorDirection)
    {
        int roomWidth = (int) roomSize.x;
        int roomHeight = (int) roomSize.y;

        switch (doorDirection)
        {
            case DirectionLib.Direction.North:
                _northDoor.OpenDoor();
                walls.SetTile(new Vector3Int(0, roomHeight / 2, 0), null);
                break;
            case DirectionLib.Direction.South:
                _southDoor.OpenDoor();
                walls.SetTile(new Vector3Int(0, -roomHeight / 2, 0), null);
                break;
            case DirectionLib.Direction.East:
                _eastDoor.OpenDoor();
                walls.SetTile(new Vector3Int(roomWidth / 2, 0, 0), null);
                break;
            case DirectionLib.Direction.West:
                _westDoor.OpenDoor();
                walls.SetTile(new Vector3Int(-roomWidth / 2, 0, 0), null);
                break;
        }
    }*/

    [SerializeField] private Tilemap _walls;
    [SerializeField] private Tilemap _intermediateWalls;

    public void SetDoor(Vector2 roomSize, DirectionLib.Direction doorDirection)
    {
        int roomWidth = (int)roomSize.x;
        int roomHeight = (int)roomSize.y;

        int midWidth = roomWidth % 2 == 0 ? (roomWidth + 1) / 2 : roomWidth / 2;
        int midHeight = roomHeight % 2 == 0 ? (roomHeight + 1) / 2 : roomHeight / 2;

        switch (doorDirection)
        {
            case DirectionLib.Direction.North:
                _walls.SetTile(new Vector3Int(0, midHeight, 0), null);
                _intermediateWalls.SetTile(new Vector3Int(0, midHeight - 1, 0), null);
                break;
            case DirectionLib.Direction.South:
                _walls.SetTile(new Vector3Int(0, roomHeight % 2 == 0 ? -midHeight + 1 : -midHeight, 0), null);
                break;
            case DirectionLib.Direction.East:
                _walls.SetTile(new Vector3Int(midWidth, 0, 0), null);
                break;
            case DirectionLib.Direction.West:
                _walls.SetTile(new Vector3Int(roomWidth % 2 == 0 ? -midWidth + 1 : -midWidth, 0, 0), null);
                break;
        }
    }
}
