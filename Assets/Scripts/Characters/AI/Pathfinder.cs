using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed partial class Pathfinder : MonoBehaviour
{
    private Vector2 _target;
    private List<Vector2> _path;
    private int _currentNodeIndex;

    [SerializeField] private float _nodePerUnit = 1;
    [SerializeField] private LayerMask _wallsMask;
    [SerializeField] private float _stopRange = 0.5f;
    [SerializeField] private Vector2 _entitySize = Vector2.one;

    public Vector2 EntitySize { get => _entitySize; }

    private float _stepBetweenNode { get => 1f / _nodePerUnit; }

    private void Awake()
    {
        _currentNodeIndex = -1;
        _path = new List<Vector2>();
    }

    public bool PathExists()
    {
        return _path.Count > 0;
    }

    public Optional<Vector2> GetNextNode()
    {
        _currentNodeIndex++;
        if (_currentNodeIndex < _path.Count)
        {
            Vector2 node = _path[_currentNodeIndex];
            return Optional<Vector2>.From(node);
        }

        return Optional<Vector2>.Empty();
    }

    private bool IsNodeNearTarget(Vector2 nodePos)
    {
        return ArePositionClose(nodePos, _target);
    }

    public bool IsPositionNearNextNode(Vector2 positon)
    {
        if (_currentNodeIndex < 0 || _currentNodeIndex >= _path.Count)
            return false;

        return ArePositionClose(positon, _path[_currentNodeIndex]);
    }

    private bool ArePositionClose(Vector2 pos1, Vector2 pos2)
    {
        return Mathf.Abs((pos2 - pos1).magnitude) <= _stopRange;
    }

    private void ReconstructPath(Node startNode, Node endNode)
    {
        Node current = endNode;
        while (current != startNode)
        {
            _path.Insert(0, current.Position);
            current = current.Parent;
        }
    }

    public bool FindPath(Vector2 targetPosition)
    {
        _currentNodeIndex = -1;
        _path.Clear();
        MinHeap<Node> openSet = new MinHeap<Node>();
        Node startNode = new Node(transform.position, _stepBetweenNode, _entitySize, _wallsMask, 0, 0);
        startNode.HCost = Node.Distance(startNode, targetPosition);
        openSet.Push(startNode);
        List<Node> closedSet = new List<Node>();

        Node current;

        while (openSet.Count > 0)
        {
            current = openSet.Pop();
            if (ArePositionClose(current.Position, targetPosition))
            {
                ReconstructPath(startNode, current);
                return true;
            }

            closedSet.Add(current);

            foreach (Node neighbour in current.GetNeighbours())
            {
                if (closedSet.Contains(neighbour) || !neighbour.IsWalkable())
                    continue;

                float distanceToNeighbour = current.GCost + Node.Distance(current, neighbour);
                if (distanceToNeighbour < neighbour.GCost)
                {
                    neighbour.GCost = distanceToNeighbour;
                    neighbour.HCost = Node.Distance(neighbour, targetPosition);
                    neighbour.Parent = current;
                    if (openSet.Contains(neighbour))
                        openSet.UpdateItem(neighbour);
                    else
                        openSet.Push(neighbour);
                }
            }
        }

        //Debug.Log($"No path was found for {gameObject.name} (id: {gameObject.GetInstanceID()}) " +
        //                 $"from {transform.position} to {targetPosition}");

        return false;
    }

#if UNITY_EDITOR
    [Header("Debug")]
    [SerializeField] private bool _debug = true;
    [SerializeField] private float _nodeSize = 0.1f;
    [SerializeField] private Color _boundsColor = Color.red;
    [SerializeField] private Color _nodeColor = Color.green;
    [SerializeField] private Color _linkColor = Color.green;

    private void OnDrawGizmos()
    {
        if (_debug)
        {
            Gizmos.color = _boundsColor;
            Gizmos.DrawWireCube(transform.position, _entitySize);
            if (_path != null)
            {
                Vector2 prevPos = transform.position;
                for (int i = _currentNodeIndex == -1 ? 0 : _currentNodeIndex; i < _path.Count; i++)
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
    }
#endif
}
