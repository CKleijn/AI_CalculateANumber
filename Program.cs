using CalculateANumber;
using System.Diagnostics;

List<int> numbersStatic = [6, 10, 25, 75, 5, 50];
int targetStatic = 728;

// Step one (DFS - Right) - Static
SetupAndRun(new StepOne(), numbersStatic, targetStatic);

// Step two (DFS - Left) - Static
SetupAndRun(new StepTwo(), numbersStatic, targetStatic);

// Step three (Greedy) - Static
SetupAndRun(new StepThree(), numbersStatic, targetStatic);

Console.WriteLine("-------------------------------------");

List<int> numbersDynamic = BaseStep.GenerateNumbers(6, 100);
long targetDynamic = BaseStep.GenerateTarget(numbersDynamic);

// Step one (DFS - Right) - Dynamic
SetupAndRun(new StepOne(), numbersDynamic, targetDynamic);

// Step two (DFS - Left) - Dynamic
SetupAndRun(new StepTwo(), numbersDynamic, targetDynamic);

// Step three (Greedy) - Dynamic
SetupAndRun(new StepThree(), numbersDynamic, targetDynamic);

static void SetupAndRun(BaseStep step, List<int> numbers, long target)
{
    Console.WriteLine($"Numbers: {string.Join(", ", numbers)}");
    Console.WriteLine($"Target: {target}");
    var tree = step.GenerateTree(numbers);
    var stopwatch = Stopwatch.StartNew();
    var nodesVisited = 0;
    step.RunSearchAlgorithm(tree.Root, target, ref nodesVisited);
    stopwatch.Stop();
    Console.WriteLine($"({step.GetType()}) Time taken: {stopwatch.ElapsedMilliseconds} ms");
    Console.WriteLine();
}
