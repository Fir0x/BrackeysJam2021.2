using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed partial class Pathfinder : MonoBehaviour
{
    private Vector2 _target;
    private List<Vector2> _path;
    private int _currentNodeIndex;

    [SerializeField] private int _nodePerUnit = 1;
    [SerializeField] private LayerMask _wallsMask;
    [SerializeField] private float _stopRange = 0.5f;

    private void Awake()
    {
        _currentNodeIndex = 0;
        _path = new List<Vector2>();
    }

    public void SetTarget(CharacterBaseClass target)
    {
        target.SubscribeOnMoveHandler(FindPath);
        //FindPath(target.transform.position);
    }

    private bool IsNodeNearTarget(Vector2 nodePos)
    {
        return ArePositionClose(nodePos, _target);
    }

    public bool IsPositionNearNextNode(Vector2 positon)
    {
        return ArePositionClose(positon, _path[_currentNodeIndex]);
    }

    private bool ArePositionClose(Vector2 pos1, Vector2 pos2)
    {
        return Mathf.Abs((pos2 - pos1).magnitude) <= _stopRange;
    }

    public Optional<Vector2> GetNextNode()
    {
        if (_currentNodeIndex < _path.Count)
        {
            Vector2 node = _path[_currentNodeIndex];
            _currentNodeIndex++;
            return Optional<Vector2>.From(node);
        }

        return Optional<Vector2>.Empty();
    }

    private void FindPath(Vector2 targetPosition)
    {
        _target = targetPosition;
        _path.Clear();
        float stepBetweenNode = 1f / _nodePerUnit;
        Vector2 nodePosition = transform.position;
        int step = 50;
        do
        {
            nodePosition += (targetPosition - nodePosition).normalized * stepBetweenNode;
            _path.Add(nodePosition);
            step--;
        } while (!IsNodeNearTarget(nodePosition) && step > 0);
    }

#if UNITY_EDITOR
    [Header("Debug")]
    [SerializeField] private bool _debug = true;
    [SerializeField] private float _nodeSize = 0.1f;
    [SerializeField] private Color _nodeColor = Color.green;
    [SerializeField] private Color _linkColor = Color.green;

    private void OnDrawGizmos()
    {
        if (_debug && _path != null)
        {
            Vector2 prevPos = transform.position;
            for (int i = _currentNodeIndex; i < _path.Count; i++)
            {
                Vector2 nextPos = _path[i];
                Gizmos.color = _nodeColor;
                Gizmos.DrawSphere(nextPos, _nodeSize);
                Gizmos.color = _linkColor;
                Gizmos.DrawLine(prevPos, nextPos);
                prevPos = nextPos;
            }
        }
    }
#endif
}
