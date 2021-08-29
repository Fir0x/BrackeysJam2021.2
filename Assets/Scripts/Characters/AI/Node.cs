using System;
using System.Collections.Generic;
using UnityEngine;

public partial class Pathfinder
{
    private class Node : IComparable, IEquatable<Node>
    {
        private Node _parent;
        private Vector2 _position;
        private float _weight;

        public Node Parent { get => _parent; set => _parent = value; }
        public Vector2 Position { get => _position; }
        public float Weight { get => _weight; }

        private float _gCost = 0;
        private float _hCost = 0;
        public float GCost { get => _gCost; set => _gCost = value; }
        public float HCost { set => _hCost = value; }
        public float FCost
        {
            get
            {
                return _gCost + _hCost;
            }
        }

        private float _step;
        private Vector2 _entitySize;
        private LayerMask _checkMask;

        private List<Node> _neighbours;

        public Node(Vector2 position, float step, Vector2 entitySize, LayerMask checkMask,
                    float weight = 0, float gCost = Mathf.Infinity, float hCost = Mathf.Infinity)
        {
            _position = position;
            _step = step;
            _entitySize = entitySize;
            _checkMask = checkMask;
            _weight = weight;
            _gCost = gCost;
            _hCost = hCost;
        }

        public static float Distance(Node a, Node b)
        {
            return Distance(a, b.Position);
        }

        public static float Distance(Node node, Vector2 otherPosition)
        {
            float distX = node.Position.x - otherPosition.x;
            float distY = node.Position.y - otherPosition.y;

            //return Mathf.Abs(distX) + Mathf.Abs(distY); // Manhattan distance
            return Mathf.Sqrt(Mathf.Pow(distX, 2) + Mathf.Pow(distY, 2)); // Euclidian distance

            //distX = Mathf.Abs(distX);
            //distY = Mathf.Abs(distY);
            //return 1 * (distX + distY) + (1 - 2 * 1) * Mathf.Min(distX, distY); // Chebyshev distance
            //return 1 * (distX + distY) + (Mathf.Sqrt(2) - 2 * 1) * Mathf.Min(distX, distY); // Octile distance
        }

        public bool IsWalkable()
        {
            return Physics2D.OverlapBox(_position, _entitySize, 0, _checkMask) == null;
        }

        public List<Node> GetNeighbours()
        {
            if (_neighbours == null)
            {
                _neighbours = new List<Node>();
                if (IsWalkable())
                {
                    int i = 0;
                    for (float y = -_step; y <= _step; y += _step)
                    {
                        for (float x = -_step; x <= _step; x += _step)
                        {
                            if (x == 0 && y == 0)
                                continue;

                            Node neighbour = new Node(new Vector2(_position.x + x, _position.y + y), _step, _entitySize, _checkMask);
                            _neighbours.Add(neighbour);
                            i++;
                        }
                    }
                }
            }

            return _neighbours;
        }

        public int CompareTo(object other)
        {
            if (other == null)
                return 1;

            if (!(other is Node))
                throw new ArgumentException("Object to compare is not a node.");

            Node otherNode = other as Node;

            if (FCost > otherNode.FCost)
                return 1;
            else if (FCost < otherNode.FCost)
                return -1;
            else
                return 0;
        }

        public bool Equals(Node other)
        {
            return other != null && _position == other.Position;
        }
    }
}