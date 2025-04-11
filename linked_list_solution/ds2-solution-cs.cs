using System;

namespace ds2_solution
{
    public class LinkedListNode
    {
        public int Value { get; set; }
        public LinkedListNode Next { get; set; }
        
        public LinkedListNode(int value)
        {
            Value = value;
            Next = null;
        }
    }
    
    public class LinkedList
    {
        private LinkedListNode head;
        
        public LinkedList()
        {
            head = null;
        }
        
        // Add a node to the end of the list
        public void Add(int value)
        {
            LinkedListNode newNode = new LinkedListNode(value);
            
            if (head == null)
            {
                head = newNode;
                return;
            }
            
            LinkedListNode current = head;
            while (current.Next != null)
            {
                current = current.Next;
            }
            
            current.Next = newNode;
        }
        
        // Create a cycle for testing purposes
        public void CreateCycle(int position)
        {
            if (head == null || position < 0)
                return;
                
            LinkedListNode cycleNode = null;
            LinkedListNode current = head;
            int index = 0;
            
            // Find the node at the specified position
            while (current.Next != null)
            {
                if (index == position)
                    cycleNode = current;
                    
                current = current.Next;
                index++;
            }
            
            // Create cycle by pointing the last node to the node at 'position'
            if (cycleNode != null)
                current.Next = cycleNode;
        }
        
        // Display the list (with cycle detection to prevent infinite loop)
        public void Display(int maxDisplay = 20)
        {
            if (head == null)
            {
                Console.WriteLine("Empty list");
                return;
            }
            
            LinkedListNode current = head;
            int count = 0;
            
            while (current != null && count < maxDisplay)
            {
                Console.Write($"{current.Value} -> ");
                current = current.Next;
                count++;
            }
            
            if (count >= maxDisplay)
                Console.WriteLine("... (possibly cyclic)");
            else
                Console.WriteLine("null");
        }
        
        // Floyd's Cycle-Finding Algorithm (Tortoise and Hare)
        public bool HasCycle()
        {
            if (head == null || head.Next == null)
                return false;
                
            LinkedListNode slow = head; // Tortoise - moves one step at a time
            LinkedListNode fast = head; // Hare - moves two steps at a time
            
            while (fast != null && fast.Next != null)
            {
                slow = slow.Next;           // Move one step
                fast = fast.Next.Next;      // Move two steps
                
                // If tortoise and hare meet, there's a cycle
                if (slow == fast)
                    return true;
            }
            
            return false;
        }
        
        // Optional: Find the start of the cycle
        public LinkedListNode FindCycleStart()
        {
            if (head == null || head.Next == null)
                return null;
                
            LinkedListNode slow = head;
            LinkedListNode fast = head;
            bool hasCycle = false;
            
            // First, detect if there's a cycle
            while (fast != null && fast.Next != null)
            {
                slow = slow.Next;
                fast = fast.Next.Next;
                
                if (slow == fast)
                {
                    hasCycle = true;
                    break;
                }
            }
            
            if (!hasCycle)
                return null;
                
            // Reset slow to head and keep fast at meeting point
            slow = head;
            
            // Move both pointers at the same pace until they meet
            while (slow != fast)
            {
                slow = slow.Next;
                fast = fast.Next;
            }
            
            return slow; // This is the start of the cycle
        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Linked List Cycle Detection");
            Console.WriteLine("===========================");
            
            // Test Case 1: No cycle
            LinkedList list1 = new LinkedList();
            list1.Add(1);
            list1.Add(2);
            list1.Add(3);
            list1.Add(4);
            list1.Add(5);
            
            Console.WriteLine("\nTest Case 1:");
            list1.Display();
            Console.WriteLine($"Has cycle: {list1.HasCycle()}");
            
            // Test Case 2: With cycle
            LinkedList list2 = new LinkedList();
            list2.Add(1);
            list2.Add(2);
            list2.Add(3);
            list2.Add(4);
            list2.Add(5);
            list2.CreateCycle(2); // Create cycle pointing back to node with value 3
            
            Console.WriteLine("\nTest Case 2:");
            list2.Display();
            Console.WriteLine($"Has cycle: {list2.HasCycle()}");
            
            LinkedListNode cycleStart = list2.FindCycleStart();
            if (cycleStart != null)
                Console.WriteLine($"Cycle starts at node with value: {cycleStart.Value}");
            
            // Interactive mode
            Console.WriteLine("\nInteractive Mode");
            Console.WriteLine("===============");
            Console.WriteLine("Create your own linked list:");
            
            LinkedList userList = new LinkedList();
            bool creating = true;
            
            while (creating)
            {
                Console.Write("\nEnter a value to add to the list (or 'done' to finish): ");
                string input = Console.ReadLine();
                
                if (input.ToLower() == "done")
                    creating = false;
                else if (int.TryParse(input, out int value))
                    userList.Add(value);
                else
                    Console.WriteLine("Invalid input. Please enter a number or 'done'.");
            }
            
            Console.Write("Create a cycle? (yes/no): ");
            string cycleChoice = Console.ReadLine();
            
            if (cycleChoice.ToLower() == "yes")
            {
                Console.Write("Enter position to create cycle at: ");
                if (int.TryParse(Console.ReadLine(), out int position))
                    userList.CreateCycle(position);
            }
            
            Console.WriteLine("\nYour linked list:");
            userList.Display();
            Console.WriteLine($"Has cycle: {userList.HasCycle()}");
            
            LinkedListNode userCycleStart = userList.FindCycleStart();
            if (userCycleStart != null)
                Console.WriteLine($"Cycle starts at node with value: {userCycleStart.Value}");
        }
    }
}