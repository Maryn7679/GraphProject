using System.Globalization;

namespace GraphProject;

internal class Program
{
    public static void Main()
    {
        //GraphVisualizer.Draw();

        //int[,] testGraph = {{0, 1, 1, 1, 1, 0 },
        //                    { 1, 0, 1, 0, 0, 0 },
        //                    { 1, 1, 0, 1, 1, 0 },
        //                    { 1, 0, 1, 0, 1, 1 },
        //                    { 1, 0, 1, 1, 0, 1 },
        //                    { 0, 0, 0, 1, 1, 0 } };

        int[,] inputMatrix = UserInterface.SetGraph();
        Graph graph = new Graph(inputMatrix);

        HashSet<int> dominationSet = DominationSetFinder.GetMinimalDominationSet(graph);
        UserInterface.PrintDominationSet(dominationSet);

        if (EulerianPathFinder.IsEulerian(graph))
        {
            Stack<int> path = EulerianPathFinder.GetEulerianCycle(graph);
            UserInterface.PrintPath(path);
        }
        else
        {
            Console.WriteLine("Graph is not Eulerian");
        }
    }
}