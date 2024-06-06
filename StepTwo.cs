using CalculateANumber.Structures;

namespace CalculateANumber
{
    public class StepTwo : BaseStep
    {
        // DFS
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

            foreach (var child in node.Children)
            {
                string newExpression = expression + (child.Operator != null ? child.Operator.ToString() : child.Value.ToString());
                if (RunSearchAlgorithm(child, target, newExpression)) return true;
            }

            return false;
        }
    }
}
