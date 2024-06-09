using CalculateANumber.Structures;

namespace CalculateANumber
{
    public class StepTwo : BaseStep
    {
        /// <summary>
        /// Depth First Search (DFS)
        /// </summary>
        /// <param name="node"></param>
        /// <param name="target"></param>
        /// <param name="expression"></param>
        /// <returns>bool</returns>
        public override bool RunSearchAlgorithm(Node node, int target, string expression = "")
        {
            // Check if node exists
            if (node == null) return false;

            // Check if DFS reached bottom of leaf
            if (node.Children.Count == 0)
            {
                // Check if expression from leaf is target
                if (target == RunExpression(expression))
                {
                    // Target found with DFS, print expression solution
                    Console.WriteLine(expression);
                    return true;
                }
            }

            // Start DFS on left side
            foreach (Node child in node.Children)
            {
                // Create new expression with child value or operator
                string newExpression = expression + (child.Operator != null ? child.Operator.ToString() : child.Value.ToString());
                // Call this recursive function to check if solution has found, otherwise go to next child of this child node
                // If solution has found > use stop condition
                if (RunSearchAlgorithm(child, target, newExpression)) return true;
            }

            // Target not found with DFS
            return false;
        }
    }
}
