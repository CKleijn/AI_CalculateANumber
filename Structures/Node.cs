namespace CalculateANumber.Structures
{
    public class Node
    {
        public int Value { get; set; }
        public char? Operator { get; set; } = default;
        public List<Node> Children { get; set; }
        public List<int> ValuesLeft { get; set; } = new List<int>();

        public Node(int value, List<int> valuesLeft)
        {
            Value = value;
            Children = new List<Node>();
            ValuesLeft = valuesLeft;
        }

        public Node(char op, List<int> valuesLeft)
        {
            Operator = op;
            Children = new List<Node>();
            ValuesLeft = valuesLeft;
        }

        public void AddChild(Node child)
        {
            Children.Add(child);
        }
    }
}
