using System;
using System.Collections.Generic;

namespace ds1_solution
{
    public class PostfixEvaluator
    {
        public static int Evaluate(string expression)
        {
            // Create a stack to store operands
            Stack<int> stack = new Stack<int>();
            
            // Split the expression by spaces to get tokens
            string[] tokens = expression.Split(' ');
            
            // Process each token
            foreach (string token in tokens)
            {
                // If the token is a number, push it onto the stack
                if (int.TryParse(token, out int number))
                {
                    stack.Push(number);
                }
                // Otherwise, it's an operator
                else
                {
                    // Pop the required number of operands from the stack
                    int b = stack.Pop(); // Second operand
                    int a = stack.Pop(); // First operand
                    
                    // Perform the operation based on the operator
                    int result = 0;
                    switch (token)
                    {
                        case "+":
                            result = a + b;
                            break;
                        case "-":
                            result = a - b;
                            break;
                        case "*":
                            result = a * b;
                            break;
                        case "/":
                            result = a / b; // Integer division
                            break;
                        default:
                            throw new ArgumentException($"Invalid operator: {token}");
                    }
                    
                    // Push the result back onto the stack
                    stack.Push(result);
                }
            }
            
            // The final result should be the only item left on the stack
            if (stack.Count != 1)
            {
                throw new InvalidOperationException("Invalid expression format");
            }
            
            return stack.Pop();
        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            // Test cases
            string[] expressions = {
                "2 3 +",                    // 5
                "4 13 5 / +",               // 6
                "10 6 9 3 + -11 * / * 17 + 5 +"  // 22
            };
            
            foreach (string expr in expressions)
            {
                try
                {
                    int result = PostfixEvaluator.Evaluate(expr);
                    Console.WriteLine($"Expression: {expr} = {result}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error evaluating {expr}: {ex.Message}");
                }
            }
            
            // Interactive mode
            Console.WriteLine("\nEnter a postfix expression (or 'exit' to quit):");
            string input = Console.ReadLine();
            
            while (input.ToLower() != "exit")
            {
                try
                {
                    int result = PostfixEvaluator.Evaluate(input);
                    Console.WriteLine($"Result: {result}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
                
                Console.WriteLine("\nEnter a postfix expression (or 'exit' to quit):");
                input = Console.ReadLine();
            }
        }
    }
}