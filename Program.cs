using System.Globalization;

namespace GraphProject;

internal class Program
{
    public static void Main()
    {
        //Test graph in matrix form. Can be pasted during input.

        //{{0, 1, 1, 1, 1, 0 },
        //{ 1, 0, 1, 0, 0, 0 },
        //{ 1, 1, 0, 1, 1, 0 },
        //{ 1, 0, 1, 0, 1, 1 },
        //{ 1, 0, 1, 1, 0, 1 },
        //{ 0, 0, 0, 1, 1, 0 } };

        Graph graph = UserInterface.SetGraph();

        HashSet<int> dominationSet = DominationSetFinder.GetMinimalDominationSet(graph);
        UserInterface.PrintDominationSet(dominationSet);

        if (EulerianPathFinder.IsEulerian(graph))
        {
            Stack<int> path = EulerianPathFinder.GetEulerianCycle(graph);
            UserInterface.PrintPath(path);
            GraphVisualizer.DrawEulerianPath(path, graph.VerticesCount(), 200);
        }
        else
        {
            Console.WriteLine("Graph is not Eulerian");
        }
    }
}