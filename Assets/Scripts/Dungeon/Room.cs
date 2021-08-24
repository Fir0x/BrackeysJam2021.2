using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    #if UNITY_EDITOR
    [SerializeField] private bool _debug;
    private Vector2 _size;

    public void SetupRoomSize(Vector2 size)
    {
        _size = size;
    }

    public void SetupRoomSize(int width, int height)
    {
        _size.x = width;
        _size.y = height;
    }

    private void OnDrawGizmos()
    {
        if (_debug)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position, _size);
            Gizmos.color = Color.blue;
            Gizmos.DrawCube(transform.position, _size - _size * 0.01f);
        }
    }
#endif
}
