# Tree

## Introduction

Trees are hierarchical data structures that consist of nodes connected by edges. Unlike linear data structures like arrays, linked lists, stacks, and queues, trees are non-linear and can represent hierarchical relationships between elements.

The most common type of tree is a binary tree, where each node has at most two children, referred to as the left child and the right child. A special type of binary tree called a Binary Search Tree (BST) follows a specific ordering property that makes searching efficient.

Trees are fundamental in computer science and have numerous applications, from representing file systems to organizing data for quick retrieval and efficient searching.

## Core Characteristics

Key characteristics of trees include:

- **Hierarchical structure**: Elements are arranged in a hierarchical manner
- **Root node**: A single node at the top of the hierarchy
- **Parent-child relationship**: Each node (except the root) has a parent node
- **No cycles**: There are no cycles or loops in a tree
- **Connected**: All nodes are reachable from the root

## Tree Terminology

Understanding tree terminology is crucial for working with tree data structures:

### Root, Node, Leaf, Edge

- **Root**: The topmost node of the tree (has no parent)
- **Node**: Each element in the tree that stores data and references to its children
- **Leaf**: A node with no children
- **Edge**: The connection between two nodes

### Parent, Child, Sibling

- **Parent**: A node that has one or more child nodes
- **Child**: A node directly connected to another node when moving away from the root
- **Sibling**: Nodes that share the same parent

### Height, Depth, Level

- **Height**: The length of the longest path from the node to a leaf
- **Depth**: The length of the path from the root to the node
- **Level**: Nodes at the same depth form a level (root is at level 0)

## Types of Trees

### Binary Trees

A binary tree is a tree where each node has at most two children, referred to as the left child and the right child.

```
      A
     / \
    B   C
   / \   \
  D   E   F
```

### Binary Search Trees (BST)

A binary search tree is a binary tree with the following properties:
- The left subtree of a node contains only nodes with keys less than the node's key
- The right subtree of a node contains only nodes with keys greater than the node's key
- Both the left and right subtrees are also binary search trees

```
      8
     / \
    3   10
   / \    \
  1   6    14
     / \   /
    4   7 13
```

### Balanced Trees (AVL, Red-Black)

Balanced trees are designed to maintain balance through rotations, ensuring optimal performance for operations:

- **AVL Tree**: A self-balancing BST where the difference in height between left and right subtrees cannot be more than 1
- **Red-Black Tree**: A self-balancing BST with additional properties that ensure logarithmic height

## Common Tree Operations

### Insert

Adding a new node to the tree while maintaining its properties.

```csharp
public void Insert(int value)
{
    root = InsertRecursive(root, value);
}

## Implementation in C#

Let's implement a basic Binary Search Tree in C#:

```csharp
using System;
using System.Collections.Generic;

public class BinarySearchTree
{
    private class Node
    {
        public int Value { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }

        public Node(int value)
        {
            Value = value;
            Left = null;
            Right = null;
        }
    }

    private Node root;

    public BinarySearchTree()
    {
        root = null;
    }

    public void Insert(int value)
    {
        root = InsertRecursive(root, value);
    }

    private Node InsertRecursive(Node node, int value)
    {
        if (node == null)
            return new Node(value);
        
        if (value < node.Value)
            node.Left = InsertRecursive(node.Left, value);
        else if (value > node.Value)
            node.Right = InsertRecursive(node.Right, value);
        
        return node;
    }

    public bool Contains(int value)
    {
        return ContainsRecursive(root, value);
    }

    private bool ContainsRecursive(Node node, int value)
    {
        if (node == null)
            return false;
        
        if (value == node.Value)
            return true;
        
        return value < node.Value 
            ? ContainsRecursive(node.Left, value) 
            : ContainsRecursive(node.Right, value);
    }

    public void InorderTraversal(Action<int> action)
    {
        InorderTraversalRecursive(root, action);
    }

    private void InorderTraversalRecursive(Node node, Action<int> action)
    {
        if (node != null)
        {
            InorderTraversalRecursive(node.Left, action);
            action(node.Value);
            InorderTraversalRecursive(node.Right, action);
        }
    }
    
    public void Delete(int value)
    {
        root = DeleteRecursive(root, value);
    }

    private Node DeleteRecursive(Node node, int value)
    {
        if (node == null)
            return null;
        
        if (value < node.Value)
            node.Left = DeleteRecursive(node.Left, value);
        else if (value > node.Value)
            node.Right = DeleteRecursive(node.Right, value);
        else
        {
            // Node with only one child or no child
            if (node.Left == null)
                return node.Right;
            else if (node.Right == null)
                return node.Left;
            
            // Node with two children
            node.Value = FindMinValue(node.Right);
            node.Right = DeleteRecursive(node.Right, node.Value);
        }
        
        return node;
    }

    private int FindMinValue(Node node)
    {
        int minValue = node.Value;
        while (node.Left != null)
        {
            minValue = node.Left.Value;
            node = node.Left;
        }
        return minValue;
    }
}

public class BSTExample
{
    public static void Main()
    {
        BinarySearchTree bst = new BinarySearchTree();
        
        // Insert values
        bst.Insert(8);
        bst.Insert(3);
        bst.Insert(10);
        bst.Insert(1);
        bst.Insert(6);
        bst.Insert(14);
        bst.Insert(4);
        bst.Insert(7);
        bst.Insert(13);
        
        // Print tree in order (should be sorted)
        Console.WriteLine("Inorder traversal:");
        bst.InorderTraversal(value => Console.Write($"{value} "));
        // Output: 1 3 4 6 7 8 10 13 14
        
        // Check if values exist
        Console.WriteLine($"\n\nContains 6: {bst.Contains(6)}");  // True
        Console.WriteLine($"Contains 11: {bst.Contains(11)}");  // False
        
        // Delete a value
        bst.Delete(6);
        Console.WriteLine("\nInorder traversal after deleting 6:");
        bst.InorderTraversal(value => Console.Write($"{value} "));
        // Output: 1 3 4 7 8 10 13 14
    }
}
```

## Common Use Cases

Trees are used in various applications:

1. **File systems**: Directories and files are organized in a tree structure
2. **Database indexing**: B-trees and their variants for efficient search and retrieval
3. **DOM in web browsers**: HTML documents are represented as a tree
4. **AI and decision making**: Decision trees for classification
5. **Network routing**: Spanning trees for routing algorithms
6. **Compiler design**: Parse trees and abstract syntax trees
7. **Computer graphics**: Scene graphs, spatial partitioning (BSP trees, quadtrees)

## Efficiency of Common Operations

### Time Complexity

For a balanced binary search tree:

| Operation | Average Case | Worst Case |
|-----------|--------------|------------|
| Search    | O(log n)     | O(n)*      |
| Insert    | O(log n)     | O(n)*      |
| Delete    | O(log n)     | O(n)*      |
| Traverse  | O(n)         | O(n)       |

*The worst case occurs in a skewed or unbalanced tree.

For a balanced tree (AVL, Red-Black), all operations have a worst-case time complexity of O(log n).

### Space Complexity

The space complexity of a tree is O(n) for storing n nodes.

The recursive implementation of tree operations also uses O(h) extra space for the call stack, where h is the height of the tree.

## Example Problem: Validating a Binary Search Tree

A common problem involving trees is determining if a binary tree is a valid binary search tree (BST).

### Problem Statement

Write a function that takes the root of a binary tree and determines if it is a valid binary search tree. A valid BST satisfies the following conditions:
- The left subtree of a node contains only nodes with keys less than the node's key
- The right subtree of a node contains only nodes with keys greater than the node's key
- Both the left and right subtrees must also be binary search trees

### Solution

```csharp
using System;

public class BSTValidator
{
    public class TreeNode
    {
        public int Value { get; set; }
        public TreeNode Left { get; set; }
        public TreeNode Right { get; set; }

        public TreeNode(int value)
        {
            Value = value;
            Left = null;
            Right = null;
        }
    }

    public static bool IsValidBST(TreeNode root)
    {
        return IsValidBSTHelper(root, long.MinValue, long.MaxValue);
    }

    private static bool IsValidBSTHelper(TreeNode node, long min, long max)
    {
        // Empty trees are valid BSTs
        if (node == null)
            return true;
        
        // Current node's value must be between min and max
        if (node.Value <= min || node.Value >= max)
            return false;
        
        // Validate left and right subtrees recursively
        return IsValidBSTHelper(node.Left, min, node.Value) && 
               IsValidBSTHelper(node.Right, node.Value, max);
    }
    
    public static void Main()
    {
        // Example 1: Valid BST
        //     2
        //    / \
        //   1   3
        TreeNode root1 = new TreeNode(2);
        root1.Left = new TreeNode(1);
        root1.Right = new TreeNode(3);
        
        Console.WriteLine($"Is Example 1 a valid BST? {IsValidBST(root1)}");  // True
        
        // Example 2: Invalid BST
        //     5
        //    / \
        //   1   4
        //      / \
        //     3   6
        TreeNode root2 = new TreeNode(5);
        root2.Left = new TreeNode(1);
        root2.Right = new TreeNode(4);
        root2.Right.Left = new TreeNode(3);
        root2.Right.Right = new TreeNode(6);
        
        Console.WriteLine($"Is Example 2 a valid BST? {IsValidBST(root2)}");  // False (4's left child is 3, which is less than 4 but not less than 5)
    }
}
```

### Explanation

1. We use a recursive approach to validate the BST property
2. For each node, we define a valid range for its value (initially from negative infinity to positive infinity)
3. As we go left, we update the upper bound to the parent's value
4. As we go right, we update the lower bound to the parent's value
5. A tree is a valid BST if all nodes satisfy their respective constraints

## Problem to Solve: Finding the Lowest Common Ancestor

Now it's your turn to solve a common tree problem!

### Problem Statement

Given a binary search tree and two nodes, write a function to find the lowest common ancestor (LCA) of the two nodes. The lowest common ancestor is defined as the lowest node in the tree that has both nodes as descendants (a node can be a descendant of itself).

### Example

Consider the following BST:
```
      6
     / \
    2   8
   / \   \
  0   4   9
     / \
    3   5
```

The LCA of nodes 2 and 8 is 6.
The LCA of nodes 2 and 4 is 2.

### Hints

1. In a BST, if both values are less than the current node's value, the LCA must be in the left subtree
2. If both values are greater than the current node's value, the LCA must be in the right subtree
3. If one value is less and one value is greater, or if the current node equals one of the values, the current node is the LCA

### Try It Yourself

Before looking at the solution, try to implement this yourself. Once you're done, you can check your solution against the provided one.

The solution is provided in the ds3-solution directory.

---

*Navigate to: [Stack](stack.md) | [Linked List](linked-list.md)*

private Node InsertRecursive(Node node, int value)
{
    if (node == null)
        return new Node(value);
    
    if (value < node.Value)
        node.Left = InsertRecursive(node.Left, value);
    else if (value > node.Value)
        node.Right = InsertRecursive(node.Right, value);
    
    return node;
}
```

### Delete

Removing a node from the tree while maintaining its properties.

```csharp
public void Delete(int value)
{
    root = DeleteRecursive(root, value);
}

private Node DeleteRecursive(Node node, int value)
{
    if (node == null)
        return null;
    
    if (value < node.Value)
        node.Left = DeleteRecursive(node.Left, value);
    else if (value > node.Value)
        node.Right = DeleteRecursive(node.Right, value);
    else
    {
        // Node with only one child or no child
        if (node.Left == null)
            return node.Right;
        else if (node.Right == null)
            return node.Left;
        
        // Node with two children
        node.Value = FindMinValue(node.Right);
        node.Right = DeleteRecursive(node.Right, node.Value);
    }
    
    return node;
}

private int FindMinValue(Node node)
{
    int minValue = node.Value;
    while (node.Left != null)
    {
        minValue = node.Left.Value;
        node = node.Left;
    }
    return minValue;
}

### Search

Checking if a value exists in the tree.

```csharp
public bool Contains(int value)
{
    return ContainsRecursive(root, value);
}

private bool ContainsRecursive(Node node, int value)
{
    if (node == null)
        return false;
    
    if (value == node.Value)
        return true;
    
    return value < node.Value 
        ? ContainsRecursive(node.Left, value) 
        : ContainsRecursive(node.Right, value);
}
```

### Traversals (Preorder, Inorder, Postorder, Level-order)

Different ways to visit all nodes in the tree:

**Inorder Traversal**: Left, Root, Right (gives nodes in sorted order for a BST)

```csharp
public void InorderTraversal(Action<int> action)
{
    InorderTraversalRecursive(root, action);
}

private void InorderTraversalRecursive(Node node, Action<int> action)
{
    if (node != null)
    {
        InorderTraversalRecursive(node.Left, action);
        action(node.Value);
        InorderTraversalRecursive(node.Right, action);
    }
}
```

**Preorder Traversal**: Root, Left, Right

```csharp
public void PreorderTraversal(Action<int> action)
{
    PreorderTraversalRecursive(root, action);
}

private void PreorderTraversalRecursive(Node node, Action<int> action)
{
    if (node != null)
    {
        action(node.Value);
        PreorderTraversalRecursive(node.Left, action);
        PreorderTraversalRecursive(node.Right, action);
    }
}
```

**Postorder Traversal**: Left, Right, Root

```csharp
public void PostorderTraversal(Action<int> action)
{
    PostorderTraversalRecursive(root, action);
}

private void PostorderTraversalRecursive(Node node, Action<int> action)
{
    if (node != null)
    {
        PostorderTraversalRecursive(node.Left, action);
        PostorderTraversalRecursive(node.Right, action);
        action(node.Value);
    }
}
```

**Level-order Traversal**: Visit nodes level by level

```csharp
public void LevelOrderTraversal(Action<int> action)
{
    if (root == null)
        return;
    
    Queue<Node> queue = new Queue<Node>();
    queue.Enqueue(root);
    
    while (queue.Count > 0)
    {
        Node node = queue.Dequeue();
        action(node.Value);
        
        if (node.Left != null)
            queue.Enqueue(node.Left);
        
        if (node.Right != null)
            queue.Enqueue(node.Right);
    }
}