using System;
using System.Collections.Generic;
using UnityEngine;

public partial class Pathfinder
{
    private class Node : IComparable<Node>
    {
        public Node parent;
        private Vector2 _position;
        private bool _isWalkable;
        private float _weight;

        public Vector2 Position { get => _position; }
        public bool IsWalkable { get => _isWalkable; }
        public float Weight { get => _weight; }

        public float gCost = 0;
        public float hCost = 0;
        public float fCost
        {
            get
            {
                return gCost + hCost;
            }
        }

        private List<Node> _neighbours;

        public Node(Vector2 position, bool isWalkable, float weight = 0)
        {
            _position = position;
            _isWalkable = isWalkable;
            _weight = weight;
        }

        public List<Node> GetNeighbours()
        {
            if (_neighbours == null)
            {

            }

            return _neighbours;
        }

        public int CompareTo(Node other)
        {
            throw new NotImplementedException();
        }
    }
}