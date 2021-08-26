using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class MinHeapTests
{
    [Test]
    public void MinHeapTestsOneElement()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        Pathfinder.MinHeap<int> heap = new Pathfinder.MinHeap<int>();
        heap.Push(200);
        Assert.AreEqual(heap.Count, 1);
        Assert.AreEqual(heap.Pop(), 200);
        Assert.AreEqual(heap.Count, 0);
    }

    [Test]
    public void MinHeapTestsThreeElement()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        Pathfinder.MinHeap<int> heap = new Pathfinder.MinHeap<int>();
        heap.Push(200);
        heap.Push(5);
        heap.Push(300);
        Assert.AreEqual(heap.Count, 3);
        Assert.AreEqual(heap.Pop(), 5);
        Assert.AreEqual(heap.Pop(), 200);
        Assert.AreEqual(heap.Pop(), 300);
        Assert.AreEqual(heap.Count, 0);
    }

    [Test]
    public void MinHeapTestsMidSize()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        Pathfinder.MinHeap<int> heap = new Pathfinder.MinHeap<int>();
        heap.Push(15);
        heap.Push(80);
        heap.Push(2);
        heap.Push(100);
        heap.Push(40);
        heap.Push(30);
        heap.Push(150);
        heap.Push(70);
        heap.Push(1);

        Assert.AreEqual(heap.Count, 9);
        Assert.AreEqual(heap.Pop(), 1);
        Assert.AreEqual(heap.Pop(), 2);
        Assert.AreEqual(heap.Pop(), 15);
        Assert.AreEqual(heap.Pop(), 30);
        Assert.AreEqual(heap.Pop(), 40);
        Assert.AreEqual(heap.Pop(), 70);
        Assert.AreEqual(heap.Count, 3);
        Assert.AreEqual(heap.Pop(), 80);
        Assert.AreEqual(heap.Pop(), 100);
        Assert.AreEqual(heap.Pop(), 150);
        Assert.AreEqual(heap.Count, 0);
    }

    [Test]
    public void MinHeapTestsRandom()
    {
        Pathfinder.MinHeap<int> heap = new Pathfinder.MinHeap<int>();
        List<int> items = new List<int>();
        for (int i = 0; i < 50; i++)
        {
            int randomElement;
            do
            {
                randomElement = Random.Range(1, 1000);
            } while (items.Contains(randomElement));

            items.Add(randomElement);
            heap.Push(randomElement);
        }

        items.Sort();
        Assert.AreEqual(items.Count, heap.Count);

        foreach (int e in items)
            Assert.AreEqual(e, heap.Pop());

        Assert.AreEqual(heap.Count, 0);
    }
}
