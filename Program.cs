using System.Globalization;

namespace GraphProject;

internal class Program
{
    public static void Main()
    {
        //var b = new GraphProject.GraphVisualizer();
        //b.Draw();
        int[, ] testGraph = {{0, 1, 1, 1, 1, 0 },
                            { 1, 0, 1, 0, 0, 0 },
                            { 1, 1, 0, 1, 1, 0 },
                            { 1, 0, 1, 0, 1, 1 },
                            { 1, 0, 1, 1, 0, 1 },
                            { 0, 0, 0, 1, 1, 0 } };

        Graph graph = new Graph(testGraph);

        var path = EulerianPathFinder.GetEulerianCycle(graph);

        foreach (var item in path)
        {
            Console.WriteLine(item.ToString());
        }
    }
}