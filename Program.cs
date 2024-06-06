using CalculateANumber;
using System.Diagnostics;

// Step one (DFS)
List<int> numbers = [6, 10, 25, 75, 5, 50];
int target = 728;
var stepOne = new StepOne();
var tree = stepOne.GenerateTree(numbers, target);
var stopwatch = Stopwatch.StartNew();
stepOne.RunSearchAlgorithm(tree.Root, target);
stopwatch.Stop();
Console.WriteLine("Time taken: " + stopwatch.ElapsedMilliseconds + " ms");

// Step two (DFS)
var stepTwo = new StepTwo();
List<int> numbers1 = stepTwo.GenerateNumbers(6, 100);
int target1 = stepTwo.GenerateTarget(numbers1);
var tree1 = stepTwo.GenerateTree(numbers1, target1);
var stopwatch1 = Stopwatch.StartNew();
stepTwo.RunSearchAlgorithm(tree1.Root, target1);
stopwatch1.Stop();
Console.WriteLine("Time taken: " + stopwatch1.ElapsedMilliseconds + " ms");

// Step three (Greedy)
var stepThree = new StepThree();
List<int> numbers2 = stepThree.GenerateNumbers(6, 100);
int target2 = stepThree.GenerateTarget(numbers2);
var tree2 = stepThree.GenerateTree(numbers2, target2);
var stopwatch2 = Stopwatch.StartNew();
stepThree.RunSearchAlgorithm(tree2.Root, target2);
stopwatch2.Stop();
Console.WriteLine("Time taken: " + stopwatch2.ElapsedMilliseconds + " ms");
