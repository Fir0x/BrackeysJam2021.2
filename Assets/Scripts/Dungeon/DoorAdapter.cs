using UnityEngine;
using UnityEngine.Tilemaps;

public class DoorAdapter : MonoBehaviour
{
    [SerializeField] private Tilemap walls;
    [SerializeField] private GameObject _northDoor;
    [SerializeField] private GameObject _southhDoor;
    [SerializeField] private GameObject _eastDoor;
    [SerializeField] private GameObject _westDoor;

    public void SetDoor(Vector2 roomSize, DirectionLib.Direction doorDirection)
    {
        int roomWidth = (int) roomSize.x;
        int roomHeight = (int) roomSize.y;

        switch (doorDirection)
        {
            case DirectionLib.Direction.North:
                _northDoor.SetActive(true);
                walls.SetTile(new Vector3Int(0, roomHeight / 2, 0), null);
                break;
            case DirectionLib.Direction.South:
                _southhDoor.SetActive(true);
                walls.SetTile(new Vector3Int(0, -roomHeight / 2, 0), null);
                break;
            case DirectionLib.Direction.East:
                _eastDoor.SetActive(true);
                walls.SetTile(new Vector3Int(roomWidth / 2, 0, 0), null);
                break;
            case DirectionLib.Direction.West:
                _westDoor.SetActive(true);
                walls.SetTile(new Vector3Int(-roomWidth / 2, 0, 0), null);
                break;
        }
    }
}
