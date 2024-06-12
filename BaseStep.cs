using CalculateANumber.Enums;
using CalculateANumber.Structures;
using System.Data;
using System.Linq.Expressions;
using System.Text;

namespace CalculateANumber
{
    public abstract class BaseStep
    {
        /// <summary>
        /// Generate specific amount of random numbers
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="maxMultipleOfTwentyFive"></param>
        /// <returns>List<int></returns>
        public static List<int> GenerateNumbers(int amount, int maxMultipleOfTwentyFive)
        {
            Random rnd = new();
            List<int> numbers = new();

            // Generate numbers until we have enough numbers
            while (numbers.Count < amount)
            {
                var type = rnd.Next(2);
                // Check which type number we want to generate and add to list
                if (type == (int)NumberOptions.ZeroToNine)
                {
                    numbers.Add(rnd.Next(1, 11));
                }
                else if (type == (int)NumberOptions.MultipleOfTwentyFive)
                {
                    int[] multipleOfTwentyFive = Enumerable.Range(25, maxMultipleOfTwentyFive).Where(n => n % 25 == 0).ToArray();
                    numbers.Add(multipleOfTwentyFive[rnd.Next(multipleOfTwentyFive.Length)]);
                }
            }

            return numbers;
        }

        /// <summary>
        /// Generate target based on given numbers and return long version of it
        /// </summary>
        /// <param name="numbers"></param>
        /// <returns>long</returns>
        public static long GenerateTarget(List<int> numbers) {
            long target = Convert.ToInt64(RunExpression(BuildExpression(numbers)));
            return (target >= 99 && target <= 1000) ? target: GenerateTarget(numbers);
        } 

        /// <summary>
        /// Generate tree based on given numbers where a search algorithm can be runned on
        /// </summary>
        /// <param name="numbers"></param>
        /// <returns>Tree</returns>
        public Tree GenerateTree(List<int> numbers)
        {
            Queue<Node> queue = new();
            Node root = new(0, numbers);
            Tree tree = new(root);

            // Add first layer of childrens to tree
            AddValueNodeAsChildAndQueue(queue, tree.Root);

            // Build tree until queue is empty
            while (queue.Count > 0)
            {
                // Get node from queue
                var existingNode = queue.Dequeue();

                // Check if node is a number or operator and call specific method
                if (existingNode.Operator == null)
                {
                    // It's a number node, so our next node needs to be an operator
                    AddOperatorNodeAsChildAndQueue(queue, existingNode);
                }
                else
                {
                    // It's a operator node, so our next node needs to be a number
                    AddValueNodeAsChildAndQueue(queue, existingNode);
                }
            }

            return tree;
        }

        /// <summary>
        /// Add value node as child of existingNode and add newNode to queue
        /// </summary>
        /// <param name="queue"></param>
        /// <param name="existingNode"></param>
        private void AddValueNodeAsChildAndQueue(Queue<Node> queue, Node existingNode)
        {
            // Loop through all valuesLeft on existingNode, add as child and enqueue new node
            foreach (int number in existingNode.ValuesLeft!)
            {
                // Calculate remaining numbers in this leaf
                List<int> numbersLeft = [.. existingNode.ValuesLeft];
                numbersLeft.Remove(number);

                Node newNode = new(number, numbersLeft);
                existingNode.AddChild(newNode);
                queue.Enqueue(newNode);
            }
        }

        /// <summary>
        /// Add operator node as child of existingNode and add newNode to queue
        /// </summary>
        /// <param name="queue"></param>
        /// <param name="existingNode"></param>
        private void AddOperatorNodeAsChildAndQueue(Queue<Node> queue, Node existingNode)
        {
            // Don't put an operator as last child
            if (existingNode.ValuesLeft.Count > 0)
            {
                // Loop through all operators, add as child and enqueue new node
                foreach (char op in new char[] { '+', '-', '/', '*' })
                {
                    Node newNode = new(op, existingNode.ValuesLeft);
                    existingNode.AddChild(newNode);
                    queue.Enqueue(newNode);
                }
            }
        }

        /// <summary>
        /// Build expression based on given numbers to ensure a foundable target
        /// </summary>
        /// <param name="numbers"></param>
        /// <returns>string</returns>
        private static string BuildExpression(List<int> numbers)
        {
            Random rnd = new();

            // Shuffle given numbers to make a more dynamic expression
            int[] shuffledNumbers = numbers.OrderBy(n => rnd.Next()).ToArray();
            char[] operators = { '+', '-', '*', '/' };

            StringBuilder expression = new();
            expression.Append(shuffledNumbers[0]);

            // Loop through shuffledNumbers and append to expression
            for (int i = 1; i < shuffledNumbers.Length; i++)
            {
                bool valid = false;

                while (!valid)
                {
                    StringBuilder calculation = new StringBuilder();
                    calculation.Append(expression.ToString());

                    char op = operators[rnd.Next(operators.Length)];
                    int nextNumber = shuffledNumbers[i];

                    // Check for division by zero
                    if (op == '/' && nextNumber == 0)
                    {
                        continue;
                    }

                    calculation.Append(op).Append(nextNumber);

                    // Check if the calculation has no decimal result
                    if (IsValidCalculation(calculation.ToString()))
                    {
                        expression.Append(op).Append(nextNumber);
                        valid = true;
                    }
                }
            }

            return expression.ToString();
        }

        /// <summary>
        /// Check if the calculation has no decimal result
        /// </summary>
        /// <param name="calculation"></param>
        /// <returns>bool</returns>
        protected static bool IsValidCalculation(string calculation)
        {
            DataTable dt = new DataTable();
            double result = Convert.ToDouble(dt.Compute(calculation, null));
            return Math.Abs(result % 1) < double.Epsilon * 100; // Check if result is close to an integer
        }

        /// <summary>
        /// Run expression string ang convert result to long
        /// </summary>
        /// <param name="expression"></param>
        /// <returns>long</returns>
        protected static long RunExpression(string expression) => Convert.ToInt64(new DataTable().Compute(expression, null));

        /// <summary>
        /// Run specific search algorithm that needs to be implemented by subclasses
        /// </summary>
        /// <param name="node"></param>
        /// <param name="target"></param>
        /// <param name="expression"></param>
        /// <returns>bool</returns>
        public abstract bool RunSearchAlgorithm(Node node, long target, ref int nodesVisited, string expression = "");
    }
}
