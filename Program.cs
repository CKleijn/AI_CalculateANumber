using CalculateANumber;
using System.Diagnostics;

int amountOfLoopsStatic = 3;
int amountOfExecutionsStatic = 0;
int amountOfErrorsStatic = 0;

List<int> numbersStatic = [6, 10, 25, 75, 5, 50];
int targetStatic = 741;

while (amountOfLoopsStatic > 0)
{
    try
    {
        // Step one (DFS - Right) - Static
        SetupAndRun(new StepOne(), numbersStatic, targetStatic);

        // Step two (DFS - Left) - Static
        SetupAndRun(new StepTwo(), numbersStatic, targetStatic);

        // Step three (Greedy) - Static
        SetupAndRun(new StepThree(), numbersStatic, targetStatic);
    }
    catch (Exception)
    {
        amountOfErrorsStatic++;
    }
    finally
    {
        amountOfExecutionsStatic += 3;
        amountOfLoopsStatic--;
    }
}

Console.WriteLine($"Error percentage: {amountOfErrorsStatic / amountOfExecutionsStatic * 100}%");

Console.WriteLine("-------------------------------------------------------------");

int amountOfLoopsDynamic = 3;
int amountOfNumbersDynamic = 6;
int amountOfExecutionsDynamic = 0;
int amountOfErrorsDynamic = 0;

while (amountOfLoopsDynamic > 0)
{
    try
    {
        List<int> numbersDynamic = BaseStep.GenerateNumbers(amountOfNumbersDynamic, 100);
        long targetDynamic = BaseStep.GenerateTarget(numbersDynamic);

        // Step one (DFS - Right) - Dynamic
        SetupAndRun(new StepOne(), numbersDynamic, targetDynamic);

        // Step two (DFS - Left) - Dynamic
        SetupAndRun(new StepTwo(), numbersDynamic, targetDynamic);

        // Step three (Greedy) - Dynamic
        SetupAndRun(new StepThree(), numbersDynamic, targetDynamic);

    }
    catch (Exception)
    {
        amountOfErrorsDynamic++;
    }
    finally
    {
        amountOfExecutionsDynamic += 3;
        amountOfLoopsDynamic--;
    }
}

Console.WriteLine($"Error percentage: {amountOfErrorsDynamic / amountOfExecutionsDynamic * 100}%");

static void SetupAndRun(BaseStep step, List<int> numbers, long target)
{
    var nodesVisited = 0;
    var tree = step.GenerateTree(numbers);

    var stopwatch = Stopwatch.StartNew();
    var run = step.RunSearchAlgorithm(tree.Root, target, ref nodesVisited);
    stopwatch.Stop();

    Console.WriteLine($"Numbers: {string.Join(", ", numbers)}");
    Console.WriteLine($"Target: {target}");
    if (!run) Console.WriteLine($"{target} has not been found with the given numbers. Please make sure the target is reachable with the given numbers.");
    Console.WriteLine($"({step.GetType()}) Time taken: {stopwatch.ElapsedMilliseconds} ms");
    Console.WriteLine();
}

