﻿using CalculateANumber.Structures;

namespace CalculateANumber
{
    public class StepOne : BaseStep
    {
        /// <summary>
        /// Depth First Search (DFS)
        /// </summary>
        /// <param name="node"></param>
        /// <param name="target"></param>
        /// <param name="nodesVisited"></param>
        /// <param name="expression"></param>
        /// <returns>bool</returns>
        public override bool RunSearchAlgorithm(Node node, long target, ref int nodesVisited, string expression = "")
        {
            // Check if node exists
            if (node == null) return false;

            // Increase nodesVisited
            nodesVisited++;

            // Check if DFS reached bottom of leaf
            if (node.Children.Count == 0)
            {
                // Check if expression from leaf is target and is a valid calculation
                if (target == RunExpression(expression) && IsValidCalculation(expression))
                {
                    // Target found with DFS, print expression solution
                    Console.WriteLine($"Expression: {expression}={target}");
                    Console.WriteLine($"Nodes visited: {nodesVisited}");
                    return true;
                }
            }

            // Reverse children to start DFS on right side
            node.Children.Reverse();

            // Start DFS on right side
            foreach (Node child in node.Children)
            {
                // Create new expression with child value or operator
                string newExpression = expression + (child.Operator != null ? child.Operator.ToString() : child.Value.ToString());
                // Call this recursive function to check if solution has found, otherwise go to next child of this child node
                // If solution has found > use stop condition
                if (RunSearchAlgorithm(child, target, ref nodesVisited, newExpression)) return true;
            }

            // Target not found with DFS
            return false;
        }
    }
}
