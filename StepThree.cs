using System.Data;
using CalculateANumber.Structures;

namespace CalculateANumber
{
    public class StepThree : BaseStep
    {
        // Greedy
        public override bool RunSearchAlgorithm(Node node, int target, string expression = "")
        {
            if (node == null) return false;

            if (node.Children.Count == 0)
            {
                if (target == RunExpression(expression))
                {
                    Console.WriteLine(expression);
                    return true;
                }
            }

            var sortedChildren = node.Children.OrderBy(child =>
            {
                if (child.Operator == null)
                {
                    var futureExpression = expression + child.Value.ToString();
                    var result = RunExpression(futureExpression);
                    return target - result;
                }
                else
                {
                    var currentResult = RunExpression(expression);
                    var difference = target - currentResult;

                    if (difference > 0 && (target - currentResult) < 100)
                        return child.Operator switch
                        {
                            '+' => 1,
                            '*' => 2,
                            '-' => 3,
                            '/' => 4,
                            _ => 0
                        };
                    else if (difference >= 100)
                        return child.Operator switch
                        {
                            '*' => 1,
                            '+' => 2,
                            '-' => 3,
                            '/' => 4,
                            _ => 0
                        };
                    else if (difference < 0 && (target - currentResult) > -100)
                        return child.Operator switch
                        {
                            '-' => 1,
                            '/' => 2,
                            '+' => 3,
                            '*' => 4,
                            _ => 0
                        };
                    else
                        return child.Operator switch
                        {
                            '/' => 1,
                            '-' => 2,
                            '+' => 3,
                            '*' => 4,
                            _ => 0
                        };
                }
            });

            foreach (var child in sortedChildren)
            {
                string newExpression = expression + (child.Operator != null ? child.Operator.ToString() : child.Value.ToString());
                if (RunSearchAlgorithm(child, target, newExpression)) return true;
            }

            return false;
        }
    }
}
