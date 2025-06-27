using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphProject;

public class Input
{
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

        Console.WriteLine("Enter edges, separated by comma: ");
        string edgesInput = Console.ReadLine();
        string[] edges = edgesInput.Split(',');

        foreach (string edge in edges)
        {
            int u = int.Parse(edge[0].ToString());
            int v = int.Parse(edge[1].ToString());
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

