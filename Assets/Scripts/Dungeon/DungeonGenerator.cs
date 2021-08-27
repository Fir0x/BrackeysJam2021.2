using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    [SerializeField] private Vector2 _roomSize;
    [SerializeField] private GameObject _coreRoomPrefab;
    [SerializeField] private List<GameObject> _roomPrefabs;

    private List<RoomData> _roomList;
    private List<RoomData> _availableRooms; // available rooms for extension

    private void Awake()
    {
        Random.InitState(544572923);
        _roomList = new List<RoomData>();
        _availableRooms = new List<RoomData>();
    }

    private void Start()
    {
        //CreateRoom();
        StartCoroutine(RoomCreationTest());
    }

    private IEnumerator RoomCreationTest()
    {
        yield return new WaitForSeconds(2);
        for (int i = 0; i < 50; i++)
        {
            CreateRoom();
            yield return null;
        }
    }

    private sealed class RoomData
    {
        private Room _room;
        private List<Vector2> _freeSpaces;
        private Vector2 _position;
        public Vector2 Position { get => _position; }

        public RoomData(Room room, Vector2 position)
        {
            _room = room;
            _position = position;

            _freeSpaces = new List<Vector2>();
            _freeSpaces.Add(position + Vector2.up);
            _freeSpaces.Add(position + Vector2.down);
            _freeSpaces.Add(position + Vector2.right);
            _freeSpaces.Add(position + Vector2.left);
        }

        public bool IsDataOf(Room room)
        {
            return _room == room;
        }

        public bool IsAvailableForExtension()
        {
            return _freeSpaces.Count > 0;
        }

        public void AddExtension(Vector2 extensionPosition)
        {
            _freeSpaces.Remove(extensionPosition);
            //print($"{_room.gameObject.name} has {_freeSpaces.Count} free spaces left");
        }

        public Vector2 GetRandomFreeSpace()
        {
            return _freeSpaces[Random.Range(0, _freeSpaces.Count)];
        }

        public bool IsNeighbour(Vector2 targetPosition)
        {
            return targetPosition + Vector2.up == _position
                || targetPosition + Vector2.down == _position
                || targetPosition + Vector2.right == _position
                || targetPosition + Vector2.left == _position;
        }

        public DoorAdapter GetDoorAdapter()
        {
            return _room.gameObject.GetComponent<DoorAdapter>();
        }
    }

    public void CreateRoom()
    {
        RoomData parentRoomData = null;
        Vector2 roomPosition;
        GameObject roomToCreate;
        if (_availableRooms.Count == 0)
        {
            roomPosition = Vector2.zero;
            roomToCreate = _coreRoomPrefab;
        }
        else
        {
            parentRoomData = _availableRooms[Random.Range(0, _availableRooms.Count)];
            roomPosition = parentRoomData.GetRandomFreeSpace();
            roomToCreate = _roomPrefabs[Random.Range(0, _roomPrefabs.Count)];
        }

        Room createdRoom = Instantiate(roomToCreate, roomPosition * _roomSize, Quaternion.identity).GetComponent<Room>();
        RoomData createdRoomData = new RoomData(createdRoom, roomPosition);

#if UNITY_EDITOR
        createdRoom.SetupRoomSize(_roomSize);
#endif

        _roomList.Add(createdRoomData);
        _availableRooms.Add(createdRoomData);

        createdRoom.gameObject.name = "Room " + _roomList.Count;

        /*foreach (RoomData roomData in _availableRooms.FindAll(room => room.IsNeighbour(roomPosition)))
        {
            DirectionLib.Direction doorDirection = DirectionLib.GetDirectionFromVector2(roomData.Position - createdRoomData.Position);
            createdRoomData.AddExtension(roomData.Position);
            createdRoom.gameObject.GetComponent<DoorAdapter>()?.SetDoor(_roomSize, doorDirection);

            roomData.AddExtension(roomPosition);
            roomData.GetDoorAdapter()?.SetDoor(_roomSize, DirectionLib.ReverseDirection(doorDirection));

            if (!roomData.IsAvailableForExtension())
                _availableRooms.Remove(roomData);
        }*/

        if (parentRoomData != null)
        {
            foreach (RoomData neighbourData in _availableRooms.FindAll(room => room.IsNeighbour(roomPosition)))
            {
                createdRoomData.AddExtension(neighbourData.Position);
                neighbourData.AddExtension(roomPosition);
                if (!neighbourData.IsAvailableForExtension())
                    _availableRooms.Remove(neighbourData);
            }

            DirectionLib.Direction doorDirection = DirectionLib.GetDirectionFromVector2(parentRoomData.Position - createdRoomData.Position);

            createdRoom.gameObject.GetComponent<DoorAdapter>()?.SetDoor(_roomSize, doorDirection);
            parentRoomData.GetDoorAdapter()?.SetDoor(_roomSize, DirectionLib.ReverseDirection(doorDirection));

            if (!parentRoomData.IsAvailableForExtension())
                _availableRooms.Remove(parentRoomData);
        }
    }
}
