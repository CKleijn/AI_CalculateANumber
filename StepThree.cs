using System.Data;
using CalculateANumber.Structures;

namespace CalculateANumber
{
    public class StepThree : BaseStep
    {
        /// <summary>
        /// Greedy Search (GS)
        /// </summary>
        /// <param name="node"></param>
        /// <param name="target"></param>
        /// <param name="expression"></param>
        /// <returns>bool</returns>
        public override bool RunSearchAlgorithm(Node node, int target, string expression = "")
        {
            // Check if node exists
            if (node == null) return false;

            // Check if GS reached bottom of leaf
            if (node.Children.Count == 0)
            {
                // Check if expression from leaf is target
                if (target == RunExpression(expression))
                {
                    // Target found with GS, print expression solution
                    Console.WriteLine(expression);
                    return true;
                }
            }

            // Heuristic (Calculate difference)
            // Order childrens based on optimal result compared to target
            IOrderedEnumerable<Node> sortedChildren = node.Children.OrderBy(child =>
            {
                // Run future expression with child value and sort based on difference between target and result
                if (child.Operator == null)
                {
                    string futureExpression = expression + child.Value.ToString();
                    int result = RunExpression(futureExpression);
                    return target - result;
                }
                else
                // Run future expression with child operator and sort based on difference between target and result
                {
                    int currentResult = RunExpression(expression);
                    int difference = target - currentResult;
                    int range = 100;

                    // Sort optimal operators based on given conditions
                    return difference switch
                    {
                        int diff when (diff > 0 && diff < range) => child.Operator switch
                        {
                            '+' => 1,
                            '*' => 2,
                            '-' => 3,
                            '/' => 4,
                            _ => 0
                        },
                        int diff when (diff >= range) => child.Operator switch
                        {
                            '*' => 1,
                            '+' => 2,
                            '-' => 3,
                            '/' => 4,
                            _ => 0
                        },
                        int diff when (diff < 0 && diff > -range) => child.Operator switch
                        {
                            '-' => 1,
                            '/' => 2,
                            '+' => 3,
                            '*' => 4,
                            _ => 0
                        },
                        _ => child.Operator switch
                        {
                            '/' => 1,
                            '-' => 2,
                            '+' => 3,
                            '*' => 4,
                            _ => 0
                        }
                    };
                }
            });

            // Start GS
            foreach (Node child in node.Children)
            {
                // Create new expression with child value or operator
                string newExpression = expression + (child.Operator != null ? child.Operator.ToString() : child.Value.ToString());
                // Call this recursive function to check if solution has found, otherwise go to next child of this child node
                // If solution has found > use stop condition
                if (RunSearchAlgorithm(child, target, newExpression)) return true;
            }

            // Target not found with GS
            return false;
        }
    }
}
