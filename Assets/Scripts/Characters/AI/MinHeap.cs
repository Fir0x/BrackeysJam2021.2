using System;
using System.Collections.Generic;
using UnityEngine;

public partial class Pathfinder
{
    public class MinHeap<T> where T : IComparable
    {
        private List<T> _items;
        private HashSet<T> _itemsChecker;
        public int Count { get => _items.Count; }

        public MinHeap()
        {
            _items = new List<T>();
            _itemsChecker = new HashSet<T>();
        }

        public void Push(T e)
        {
            _items.Add(e);
            SortBottomTop();
        }

        public T Pop()
        {
            T result = _items[0];
            _items[0] = _items[_items.Count - 1];
            _items.RemoveAt(_items.Count - 1);

            SortTopBottom();

            return result;
        }

        private void Swap(int i, int j)
        {
            T tmp = _items[i];
            _items[i] = _items[j];
            _items[j] = tmp;
        }

        private void SortBottomTop()
        {
            int i = _items.Count - 1;
            int father = i / 2;
            while (i > 0 && _items[i].CompareTo(_items[father]) < 0)
            {
                Swap(i, father);
                i = father;
                father = i / 2;
            }
        }

        private void SortTopBottom()
        {
            if (_items.Count < 2)
                return;

            int i = 0;
            bool isPlaced = false;

            while (_items.Count / 2 > i && !isPlaced)
            {
                int leftChild = i * 2 + 1;
                int rightChild = i * 2 + 2;
                int swapIndex;
                if (rightChild < _items.Count)
                    swapIndex = _items[leftChild].CompareTo(_items[rightChild]) < 0 ? leftChild : rightChild;
                else
                    swapIndex = leftChild;

                if (_items[i].CompareTo(_items[swapIndex]) > 0)
                {
                    Swap(i, swapIndex);
                    i = swapIndex;
                }
                else
                    isPlaced = true;
            }
        }

        public bool Contains(T e)
        {
            return _itemsChecker.Contains(e);
        }
    }
}
