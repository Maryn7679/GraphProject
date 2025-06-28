using System.Globalization;

namespace GraphProject;

internal class Program
{
    public static void Main()
    {
        GraphVisualizer.Draw();

        //int[,] testGraph = {{0, 1, 1, 1, 1, 0 },
        //                    { 1, 0, 1, 0, 0, 0 },
        //                    { 1, 1, 0, 1, 1, 0 },
        //                    { 1, 0, 1, 0, 1, 1 },
        //                    { 1, 0, 1, 1, 0, 1 },
        //                    { 0, 0, 0, 1, 1, 0 } };

        //int[,] inputMatrix = UserInterface.InputGraph();
        //Graph graph = new Graph(inputMatrix);

        //var path = EulerianPathFinder.GetEulerianCycle(graph);
        //UserInterface.PrintPath(path);
    }
}