using System;

namespace ds3_solution
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
    
    public class BinarySearchTree
    {
        private TreeNode root;
        
        public BinarySearchTree()
        {
            root = null;
        }
        
        // Insert a value into the BST
        public void Insert(int value)
        {
            root = InsertRecursive(root, value);
        }
        
        private TreeNode InsertRecursive(TreeNode node, int value)
        {
            if (node == null)
                return new TreeNode(value);
            
            if (value < node.Value)
                node.Left = InsertRecursive(node.Left, value);
            else if (value > node.Value)
                node.Right = InsertRecursive(node.Right, value);
            
            return node;
        }
        
        // Display the tree in order
        public void InOrderTraversal()
        {
            Console.Write("In-order traversal: ");
            InOrderTraversalRecursive(root);
            Console.WriteLine();
        }
        
        private void InOrderTraversalRecursive(TreeNode node)
        {
            if (node != null)
            {
                InOrderTraversalRecursive(node.Left);
                Console.Write($"{node.Value} ");
                InOrderTraversalRecursive(node.Right);
            }
        }
        
        // Find Lowest Common Ancestor of two nodes
        public TreeNode FindLCA(int n1, int n2)
        {
            return FindLCARecursive(root, n1, n2);
        }
        
        private TreeNode FindLCARecursive(TreeNode node, int n1, int n2)
        {
            // Base case
            if (node == null)
                return null;
            
            // If both n1 and n2 are greater than node's value, LCA lies in right subtree
            if (node.Value < n1 && node.Value < n2)
                return FindLCARecursive(node.Right, n1, n2);
            
            // If both n1 and n2 are less than node's value, LCA lies in left subtree
            if (node.Value > n1 && node.Value > n2)
                return FindLCARecursive(node.Left, n1, n2);
            
            // If one value is less and one is greater, or if the current node equals one of the values,
            // then the current node is the LCA
            return node;
        }
        
        // Visualization helper to print tree structure
        public void PrintTree()
        {
            Console.WriteLine("Tree Structure:");
            PrintTreeRecursive(root, "", true);
        }
        
        private void PrintTreeRecursive(TreeNode node, string indent, bool last)
        {
            if (node != null)
            {
                Console.Write(indent);
                if (last)
                {
                    Console.Write("└── ");
                    indent += "    ";
                }
                else
                {
                    Console.Write("├── ");
                    indent += "│   ";
                }
                
                Console.WriteLine(node.Value);
                
                if (node.Left != null || node.Right != null)
                {
                    if (node.Left != null && node.Right != null)
                    {
                        PrintTreeRecursive(node.Left, indent, false);
                        PrintTreeRecursive(node.Right, indent, true);
                    }
                    else if (node.Left != null)
                    {
                        PrintTreeRecursive(node.Left, indent, true);
                    }
                    else
                    {
                        PrintTreeRecursive(node.Right, indent, true);
                    }
                }
            }
        }
        
        // Find a node by value
        public TreeNode Find(int value)
        {
            return FindRecursive(root, value);
        }
        
        private TreeNode FindRecursive(TreeNode node, int value)
        {
            if (node == null || node.Value == value)
                return node;
                
            if (value < node.Value)
                return FindRecursive(node.Left, value);
            else
                return FindRecursive(node