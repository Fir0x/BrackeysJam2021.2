using UnityEngine;
using UnityEngine.Tilemaps;

public class DoorAdapter : MonoBehaviour
{
    [SerializeField] private Tilemap walls;
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
    }
}
