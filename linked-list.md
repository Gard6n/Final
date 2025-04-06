# Linked List

## Introduction

A linked list is a dynamic data structure that consists of a sequence of elements, called nodes, where each node contains data and a reference (or link) to the next node in the sequence. Unlike arrays, linked lists don't require contiguous memory allocation, making them flexible for dynamic memory management.

The beauty of linked lists lies in their ability to grow and shrink during program execution without the need for memory reallocation, which can be a costly operation for large datasets.

## Core Characteristics

Key characteristics of linked lists include:

- **Dynamic size**: Can grow or shrink at runtime
- **Non-contiguous memory**: Nodes can be scattered throughout memory
- **Sequential access**: Elements must be accessed in sequence (no direct indexing)
- **No fixed size limit**: Limited only by available memory
- **Efficient insertions and deletions**: When position is known

## Types of Linked Lists

### Singly Linked List

In a singly linked list, each node points to the next node in the sequence. The last node points to null, indicating the end of the list.

```
[Data|Next] -> [Data|Next] -> [Data|Next] -> null
```

### Doubly Linked List

In a doubly linked list, each node contains references to both the next and previous nodes, allowing traversal in both directions.

```
null <- [Prev|Data|Next] <-> [Prev|Data|Next] <-> [Prev|Data|Next] -> null
```

### Circular Linked List

A circular linked list is a variation where the last node points back to the first node, creating a circle.

```
     ┌────────────────────────────┐
     ↓                            │
[Data|Next] -> [Data|Next] -> [Data|Next]
```

## Common Linked List Operations

### Insert

Inserting elements can be done at the beginning, end, or at a specific position in the list.

```csharp
// Insert at beginning
public void InsertAtBeginning(T data)
{
    Node<T> newNode = new Node<T>(data);
    newNode.Next = head;
    head = newNode;
}
```

### Delete

Deleting elements can also be performed at the beginning, end, or at a specific position.

```csharp
// Delete from beginning
public T DeleteFromBeginning()
{
    if (head == null)
        throw new InvalidOperationException("The list is empty");
    
    T data = head.Data;
    head = head.Next;
    return data;
}
```

### Search

Searching involves traversing the list to find a specific element.

```csharp
public bool Contains(T data)
{
    Node<T> current = head;
    while (current != null)
    {
        if (EqualityComparer<T>.Default.Equals(current.Data, data))
            return true;
        current = current.Next;
    }
    return false;
}
```

### Traverse

Traversal involves visiting each node in the list.

```csharp
public void Traverse(Action<T> action)
{
    Node<T> current = head;
    while (current != null)
    {
        action(current.Data);
        current = current.Next;
    }
}
```

## Implementation in C#

### Using Built-in LinkedList Class

C# provides a built-in `LinkedList<T>` class in the System.Collections.Generic namespace, which implements a doubly linked list.

```csharp
using System;
using System.Collections.Generic;

LinkedList<string> names = new LinkedList<string>();

// Adding elements
names.AddLast("Alice");
names.AddLast("Bob");
names.AddFirst("Charlie");  // Will be at the beginning

// Traversing
foreach (string name in names)
{
    Console.WriteLine(name);
}

// Output:
// Charlie
// Alice
// Bob

// Finding and inserting
LinkedListNode<string> bobNode = names.Find("Bob");
names.AddBefore(bobNode, "David");

// Removing
names.Remove("Alice");
```

### Creating a Custom Linked List

While the built-in LinkedList class is convenient, implementing your own helps understand the underlying mechanics:

```csharp
public class Node<T>
{
    public T Data { get; set; }
    public Node<T> Next { get; set; }

    public Node(T data)
    {
        Data = data;
        Next = null;
    }
}

public class SinglyLinkedList<T>
{
    private Node<T> head;
    private int count;

    public int Count => count;
    public bool IsEmpty => count == 0;

    public void AddFirst(T data)
    {
        Node<T> newNode = new Node<T>(data);
        newNode.Next = head;
        head = newNode;
        count++;
    }

    public void AddLast(T data)
    {
        Node<T> newNode = new Node<T>(data);
        
        if (head == null)
        {
            head = newNode;
        }
        else
        {
            Node<T> current = head;
            while (current.Next != null)
            {
                current = current.Next;
            }
            current.Next = newNode;
        }
        
        count++;
    }

    public bool Remove(T data)
    {
        if (head == null)
            return false;
        
        if (EqualityComparer<T>.Default.Equals(head.Data, data))
        {
            head = head.Next;
            count--;
            return true;
        }
        
        Node<T> current = head;
        while (current.Next != null)
        {
            if (EqualityComparer<T>.Default.Equals(current.Next.Data, data))
            {
                current.Next = current.Next.Next;
                count--;
                return true;
            }
            current = current.Next;
        }
        
        return false;
    }

    public void Clear()
    {
        head = null;
        count = 0;
    }

    public bool Contains(T data)
    {
        Node<T> current = head;
        while (current != null)
        {
            if (EqualityComparer<T>.Default.Equals(current.Data, data))
                return true;
            current = current.Next;
        }
        return false;
    }

    public void ForEach(Action<T> action)
    {
        Node<T> current = head;
        while (current != null)
        {
            action(current.Data);
            current = current.Next;
        }
    }
}
```

## Common Use Cases

Linked lists are used in various scenarios:

1. **Implementation of other data structures**: Such as stacks, queues, and hash tables
2. **Memory management**: For managing free blocks in memory allocation systems
3. **Undo functionality**: In applications like text editors
4. **Music playlists**: Where songs can be easily added or removed
5. **Browser history**: For navigating forward and backward through web pages
6. **Symbol tables in compilers**: Where symbols need frequent insertions and deletions

## Efficiency of Common Operations

### Time Complexity

| Operation               | Singly Linked List | Doubly Linked List |
|-------------------------|--------------------|--------------------|
| Access (by index)       | O(n)               | O(n)               |
| Insert/Delete at start  | O(1)               | O(1)               |
| Insert/Delete at end    | O(n)*              | O(1)**             |
| Insert/Delete in middle | O(n)               | O(n)               |
| Search                  | O(n)               | O(n)               |

*O(1) if we maintain a tail pointer
**Assuming a tail pointer is maintained

### Space Complexity

- Singly Linked List: O(n) space for n elements, plus one pointer per node
- Doubly Linked List: O(n) space for n elements, plus two pointers per node

## Example Problem: Implementing a Queue with a Linked List

Queues follow the First In, First Out (FIFO) principle and can be efficiently implemented using a linked list.

### Problem Statement

Implement a Queue data structure using a linked list that supports the following operations:
- `Enqueue(item)`: Add an item to the back of the queue
- `Dequeue()`: Remove and return the item at the front of the queue
- `Peek()`: Return the item at the front without removing it
- `IsEmpty()`: Check if the queue is empty
- `Count()`: Return the number of items in the queue

### Solution

```csharp
using System;

public class LinkedListQueue<T>
{
    private class Node
    {
        public T Data { get; }
        public Node Next { get; set; }

        public Node(T data)
        {
            Data = data;
            Next = null;
        }
    }

    private Node front;
    private Node rear;
    private int count;

    public LinkedListQueue()
    {
        front = null;
        rear = null;
        count = 0;
    }

    public int Count => count;

    public bool IsEmpty => count == 0;

    public void Enqueue(T item)
    {
        Node newNode = new Node(item);
        
        if (rear == null)
        {
            front = newNode;
            rear = newNode;
        }
        else
        {
            rear.Next = newNode;
            rear = newNode;
        }
        
        count++;
    }

    public T Dequeue()
    {
        if (IsEmpty)
            throw new InvalidOperationException("Queue is empty");
        
        T data = front.Data;
        front = front.Next;
        
        count--;
        
        if (front == null)
            rear = null;
        
        return data;
    }

    public T Peek()
    {
        if (IsEmpty)
            throw new InvalidOperationException("Queue is empty");
        
        return front.Data;
    }
}

public class QueueExample
{
    public static void Main()
    {
        LinkedListQueue<string> queue = new LinkedListQueue<string>();
        
        queue.Enqueue("First");
        queue.Enqueue("Second");
        queue.Enqueue("Third");
        
        Console.WriteLine($"Front item: {queue.Peek()}");  // Output: First
        Console.WriteLine($"Dequeued item: {queue.Dequeue()}");  // Output: First
        Console.WriteLine($"New front item: {queue.Peek()}");  // Output: Second
        Console.WriteLine($"Queue size: {queue.Count}");  // Output: 2
    }
}
```

### Explanation

1. We define a nested `Node` class to represent elements in our linked list
2. We maintain references to both the front and rear of the queue for efficient operations
3. `Enqueue` adds a new node to the rear of the queue
4. `Dequeue` removes and returns the node at the front
5. `Peek` returns the data at the front without removing it
6. The implementation ensures O(1) time complexity for all operations

## Problem to Solve: Detecting a Cycle in a Linked List

Now it's your turn to solve a classic linked list problem!

### Problem Statement

Write a function that detects if a linked list contains a cycle (a node that points back to a previous node, creating a loop). Your function should return true if a cycle exists and false otherwise.

### Example

Consider the following linked list:
```
1 -> 2 -> 3 -> 4 -> 5 -> 3 (points back to node with value 3)
```

Your function should return true because there is a cycle where the last node points back to the node with value 3.

### Hints

1. You can use Floyd's Cycle-Finding Algorithm (also known as the "tortoise and hare" algorithm)
2. Use two pointers, one moving one step at a time and the other moving two steps at a time
3. If there's a cycle, the faster pointer will eventually catch up to the slower one
4. If there's no cycle, the faster pointer will reach the end of the list

### Try It Yourself

Before looking at the solution, try to implement this yourself. 
The solution is provided in the linked-list directory.

---

*Navigate to: [Stack](stack.md) | [Tree](tree.md)*