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
int targetDynamic = BaseStep.GenerateTarget(numbersDynamic);

// Step one (DFS - Right) - Dynamic
SetupAndRun(new StepOne(), numbersDynamic, targetDynamic);

// Step two (DFS - Left) - Dynamic
SetupAndRun(new StepTwo(), numbersDynamic, targetDynamic);

// Step three (Greedy) - Dynamic
SetupAndRun(new StepThree(), numbersDynamic, targetDynamic);



static void SetupAndRun(BaseStep step, List<int> numbers, int target)
{
    var tree = step.GenerateTree(numbers);
    var stopwatch = Stopwatch.StartNew();
    step.RunSearchAlgorithm(tree.Root, target);
    stopwatch.Stop();
    Console.WriteLine($"({step.GetType()}) Time taken: {stopwatch.ElapsedMilliseconds} ms");
}
