using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphProject;

public class UserInterface
{
    public static void PrintPath(Stack<int> path)
    {
        string outputString = "";
        foreach (var vertex in path)
        {
            outputString += (vertex.ToString() + " -> ");
        }
        Console.WriteLine($"Eulerian path: {outputString}");
    }

    public static int[,] InputGraph()
    {
        Console.WriteLine("Select input method (Random, Edges, Matrix): ");
        string input = Console.ReadLine();
        int[,] graph = ParseUserInput(input);
        return graph;
    }

    private static int[,] ParseUserInput(string input) =>
        input switch
        {
            //"Random" => GenerateGraph(),
            "Edges" => InputEdges(),
            //"Matrix" => InputMatrix(),
            _ => throw new ArgumentException("Invalid string value for command", nameof(input)),
        };

    private static int[,] InputEdges()
    {
        Console.WriteLine("Enter number of vertices: ");
        string verticesCountInput = Console.ReadLine();
        int verticesCount = int.Parse(verticesCountInput);
        int[,] matrix = new int[verticesCount, verticesCount];

        Console.WriteLine("Enter edges in u-v format, separated by comma: ");
        string edgesInput = Console.ReadLine();
        string[] edges = edgesInput.Split(',');

        foreach (string edge in edges)
        {
            string[] edgeArray = edge.Split('-');

            int u = int.Parse(edgeArray[0]);
            int v = int.Parse(edgeArray[1]);
            matrix[u, v] = 1;
            matrix[v, u] = 1;
        }

        return matrix;
    }

    //private static int[,] InputMatrix()
    //{
    //}

    //private static int[,] GenerateGraph()
    //{
    //    Console.WriteLine("Enter number of vertices: ");
    //    string verticesCountInput = Console.ReadLine();
    //    int verticesCount = int.Parse(verticesCountInput);
    //    int[,] matrix = new int[verticesCount, verticesCount];
    //}
}

