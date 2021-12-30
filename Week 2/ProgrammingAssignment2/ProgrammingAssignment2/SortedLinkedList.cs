using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortedLinkedList<T> : LinkedList<T> where T : IComparable
{

    public SortedLinkedList() : base()
    {
    }

    public void Add(T item)
    {
        // adding to empty list
        if (Count == 0)
        {
            AddFirst(item);
        }
        else if (First.Value.CompareTo(item) >= 0)
        {
            // adding before head
            AddFirst(item);
        }
        else
        {
            // find place to add new node
            LinkedListNode<T> previousNode = null;
            LinkedListNode<T> currentNode = First;
            while (currentNode != null &&
                currentNode.Value.CompareTo(item) < 0)
            {
                previousNode = currentNode;
                currentNode = currentNode.Next;
            }

            // link in new node between previous node and current node
            AddAfter(previousNode, item);
        }
    }

    public void Reposition(T item)
    {
        // move item forward into correct position
        LinkedListNode<T> currentNode = Find(item);
        while (currentNode.Previous != null &&
            currentNode.Value.CompareTo(currentNode.Previous.Value) < 0)
        {
            currentNode.Value = currentNode.Previous.Value;
            currentNode.Previous.Value = item;
            currentNode = currentNode.Previous;
        }
    }
}