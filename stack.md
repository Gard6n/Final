# Stack

## Introduction

A stack is one of the most fundamental data structures in computer science. It follows the Last In, First Out (LIFO) principle - the last element added to the stack is the first one to be removed. Think of a stack like a pile of plates: you can only add or remove plates from the top of the pile.

The name "stack" comes from the real-world analogy of stacking physical objects on top of each other. This simple yet powerful concept has numerous applications in computing, from function call management to expression evaluation.

## Core Characteristics

The key characteristics that define a stack are:

- **LIFO (Last In, First Out)**: The most recently added element is the first one to be removed
- **Restricted access**: Elements can only be accessed from one end (the "top" of the stack)
- **Two primary operations**: Push (add an element) and Pop (remove an element)

## Common Stack Operations

### Push

The `Push` operation adds an element to the top of the stack.

```csharp
stack.Push(element);
```

### Pop

The `Pop` operation removes and returns the top element from the stack.

```csharp
T element = stack.Pop();
```

### Peek

The `Peek` operation returns the top element without removing it from the stack.

```csharp
T element = stack.Peek();
```

### Size/IsEmpty

These operations check the current number of elements or whether the stack is empty.

```csharp
int count = stack.Count;
bool isEmpty = stack.Count == 0;
```

## Implementation in C#

C# provides a built-in `Stack<T>` class in the System.Collections.Generic namespace. Here's how to use it:

```csharp
using System;
using System.Collections.Generic;

Stack<int> numberStack = new Stack<int>();
numberStack.Push(10);
numberStack.Push(20);
numberStack.Push(30);

Console.WriteLine($"Top element: {numberStack.Peek()}");  // Output: 30
Console.WriteLine($"Popped element: {numberStack.Pop()}");  // Output: 30
Console.WriteLine($"New top element: {numberStack.Peek()}");  // Output: 20
```

### Creating a Custom Stack

While using the built-in Stack class is convenient, implementing your own stack helps to understand the underlying mechanics:

```csharp
public class CustomStack<T>
{
    private T[] _items;
    private int _count;
    private const int DefaultCapacity = 4;

    public CustomStack()
    {
        _items = new T[DefaultCapacity];
        _count = 0;
    }

    public int Count => _count;

    public void Push(T item)
    {
        if (_count == _items.Length)
            Resize();
        
        _items[_count++] = item;
    }

    public T Pop()
    {
        if (_count == 0)
            throw new InvalidOperationException("The stack is empty");
        
        T item = _items[--_count];
        _items[_count] = default; // Help garbage collection
        return item;
    }

    public T Peek()
    {
        if (_count == 0)
            throw new InvalidOperationException("The stack is empty");
        
        return _items[_count - 1];
    }

    private void Resize()
    {
        T[] newItems = new T[_items.Length * 2];
        Array.Copy(_items, newItems, _count);
        _items = newItems;
    }
}
```

## Common Use Cases

Stacks are widely used in various programming scenarios:

1. **Function call management**: The call stack keeps track of function calls and their context
2. **Expression evaluation**: Evaluating arithmetic expressions, especially in compilers
3. **Undo functionality**: Tracking operations that can be undone in applications
4. **Backtracking algorithms**: Such as depth-first search in graphs
5. **Parsing**: Used in syntax analysis for programming languages

## Efficiency of Common Operations

### Time Complexity

| Operation | Time Complexity |
|-----------|----------------|
| Push      | O(1)           |
| Pop       | O(1)           |
| Peek      | O(1)           |
| Search    | O(n)           |

The standard stack operations (push, pop, and peek) are extremely efficient with constant time complexity O(1). However, searching for an element in a stack is inefficient, requiring O(n) time in the worst case as you would need to pop elements one by one until finding the target.

### Space Complexity

The space complexity of a stack is O(n), where n is the number of elements in the stack.

## Example Problem: Balancing Parentheses

A classic problem that can be elegantly solved using a stack is checking whether a string of parentheses is balanced.

### Problem Statement

Write a function that determines if a string containing only parentheses characters - `(`, `)`, `{`, `}`, `[`, `]` - is balanced. A string is considered balanced if:

1. Open brackets must be closed by the same type of brackets
2. Open brackets must be closed in the correct order
3. Every close bracket has a corresponding open bracket of the same type

### Solution

```csharp
using System;
using System.Collections.Generic;

public class ParenthesesBalancer
{
    public static bool IsBalanced(string s)
    {
        Stack<char> stack = new Stack<char>();
        
        foreach (char c in s)
        {
            if (c == '(' || c == '{' || c == '[')
            {
                stack.Push(c);
            }
            else if (c == ')' || c == '}' || c == ']')
            {
                if (stack.Count == 0)
                    return false;
                
                char open = stack.Pop();
                if ((c == ')' && open != '(') || 
                    (c == '}' && open != '{') || 
                    (c == ']' && open != '['))
                {
                    return false;
                }
            }
        }
        
        return stack.Count == 0;
    }
    
    public static void Main()
    {
        string[] testCases = {
            "()",
            "()[]{}",
            "(]",
            "([)]",
            "{[]}"
        };
        
        foreach (string test in testCases)
        {
            Console.WriteLine($"'{test}' is {(IsBalanced(test) ? "balanced" : "not balanced")}");
        }
    }
}
```

### Explanation

1. We iterate through each character in the string
2. If we encounter an opening bracket, we push it onto the stack
3. If we encounter a closing bracket, we check if it matches the most recent opening bracket (which should be at the top of the stack)
4. If the brackets don't match or if we try to pop from an empty stack, the string is not balanced
5. Finally, if the stack is empty after processing all characters, the string is balanced

### Output

```
'()' is balanced
'()[]{}' is balanced
'(]' is not balanced
'([)]' is not balanced
'{[]}' is balanced
```

## Problem to Solve: Evaluating Postfix Expressions

Now it's your turn to solve a problem using stacks!

### Problem Statement

Write a function that evaluates a postfix (also known as Reverse Polish Notation) expression. In postfix notation, operators follow their operands. For example, "2 3 +" means "2 + 3 = 5".

Rules:
1. The expression will be a string with numbers and operators separated by spaces
2. Valid operators are +, -, *, and /
3. All numbers are non-negative integers
4. Division between two integers should truncate toward zero (integer division)
5. It is guaranteed that the given expression is valid

### Example

Input: "2 3 +"
Output: 5

Input: "4 13 5 / +"
Output: 6 (because 13 / 5 = 2, then 4 + 2 = 6)

Input: "10 6 9 3 + -11 * / * 17 + 5 +"
Output: 22

### Hints

1. Use a stack to keep track of the operands
2. Iterate through each token in the expression
3. If the token is a number, push it onto the stack
4. If the token is an operator, pop the required number of operands from the stack, perform the operation, and push the result back onto the stack
5. The final result should be the only item left on the stack

### Try It Yourself

Before looking at the solution, try to implement this yourself.
The solution is provided in the stack directory.

---

*Navigate to: [Linked List](linked-list.md) | [Tree](tree.md)*