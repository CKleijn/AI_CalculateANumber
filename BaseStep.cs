using CalculateANumber.Enums;
using CalculateANumber.Structures;
using System.Data;
using System.Text;

namespace CalculateANumber
{
    public abstract class BaseStep
    {
        public List<int> GenerateNumbers(int amount, int maxMultipleOfTwentyFive)
        {
            var rnd = new Random();
            List<int> numbers = new();

            while (numbers.Count < amount)
            {
                var type = rnd.Next(2);
                int drawn = 0;
                if (type == (int)NumberOptions.ZeroToNine)
                {
                    drawn = rnd.Next(1, 11);
                }
                else if (type == (int)NumberOptions.MultipleOfTwentyFive)
                {
                    int[] multipleOfTwentyFive = Enumerable.Range(25, maxMultipleOfTwentyFive).Where(n => n % 25 == 0).ToArray();
                    drawn = multipleOfTwentyFive[rnd.Next(multipleOfTwentyFive.Length)];
                }

                if (!numbers.Contains(drawn))
                {
                    numbers.Add(drawn);
                }
            }

            return numbers.ToList();
        }

        public int GenerateTarget(List<int> numbers)
        {
            return Convert.ToInt32(RunExpression(BuildExpression(numbers)));
        }

        public Tree GenerateTree(List<int> numbers, int target)
        {
            char[] operators = { '+', '-', '/', '*' };
            var queue = new Queue<Node>();
            var startNode = new Node(0, numbers);
            var tree = new Tree(startNode);

            foreach (var number in numbers)
            {
                // Needs to be unique
                var numbersLeft = numbers.Where(x => x != number).ToList();
                var newNode = new Node(number, numbersLeft);
                queue.Enqueue(newNode);
                tree.Root.AddChild(newNode);
            }

            while (queue.Count > 0)
            {
                var existingNode = queue.Dequeue();

                if (existingNode.Operator == null)
                {
                    if (existingNode.ValuesLeft.Count > 0)
                    {
                        foreach (var op in operators)
                        {
                            var newNode = new Node(op, existingNode.ValuesLeft);
                            existingNode.AddChild(newNode);
                            queue.Enqueue(newNode);
                        }
                    }
                }
                else
                {
                    foreach (var number in existingNode.ValuesLeft!)
                    {
                        var numbersLeft = existingNode.ValuesLeft.Where(x => x != number).ToList();
                        var newNode = new Node(number, numbersLeft);
                        existingNode.AddChild(newNode);
                        queue.Enqueue(newNode);
                    }
                }
            }

            return tree;
        }

        public abstract bool RunSearchAlgorithm(Node node, int target, string expression = "");

        private string BuildExpression(List<int> numbers)
        {
            var rnd = new Random();
            var shuffledNumbers = numbers.OrderBy(n => rnd.Next()).ToArray();
            char[] expressions = { '+', '-', '/', '*' };

            var expression = new StringBuilder();

            for (int i = 0; i < shuffledNumbers.Length - 1; i += 2)
            {
                expression.Append(shuffledNumbers[i]);
                expression.Append(expressions[rnd.Next(expressions.Length)]);
                expression.Append(shuffledNumbers[i + 1]);

                if (i != (shuffledNumbers.Length - 2))
                {
                    expression.Append(expressions[rnd.Next(expressions.Length)]);
                }
            }

            return expression.ToString();
        }

        protected int RunExpression(string expression)
        {
            var result = new DataTable().Compute(expression, null);
            return Convert.ToInt32(result);
        }
    }
}
