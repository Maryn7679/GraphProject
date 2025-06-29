using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphProject;

public class UserInterface
{
    public static void PrintPath(Stack<int> path)
    {
        string outputString = path.Pop().ToString();
        foreach (var vertex in path)
        {
            outputString = vertex.ToString() + " -> " + outputString;
        }
        Console.WriteLine($"Eulerian path: {outputString}");
    }

    public static void PrintDominationSet(HashSet<int> set)
    {
        string outputString = "";
        foreach (int vertex in set)
        {
            outputString += vertex.ToString() + ", ";
        }
        Console.WriteLine($"Domination set: {outputString}");
    }

    public static Graph SetGraph()
    {
        Console.WriteLine("Select set method (Random, Edges, Matrix): ");
        string input = Console.ReadLine();
        Graph graph = ParseUserInput(input);
        return graph;
    }

    private static Graph ParseUserInput(string input) =>
        input switch
        {
            "Random" => GenerateGraph(),
            "r" => GenerateGraph(),
            "Edges" => InputEdges(),
            "e" => InputEdges(),
            //"Matrix" => InputMatrix(),
            //"m" => InputMatrix(),
            _ => throw new ArgumentException("Invalid string value for command", nameof(input)),
        };

    private static Graph InputEdges()
    {
        Console.WriteLine("Enter graph type (Standard, Directional): ");
        string graphTypeInput = Console.ReadLine();

        Console.WriteLine("Enter number of vertices: ");
        string verticesCountInput = Console.ReadLine();
        int verticesCount = int.Parse(verticesCountInput);
        int[,] matrix = new int[verticesCount, verticesCount];

        Console.WriteLine("Enter edges in u-v format, separated by comma: ");
        string edgesInput = Console.ReadLine();
        string[] edges = edgesInput.Split(',');

        if (graphTypeInput == "Standard" ||
            graphTypeInput == "s")
        {
            foreach (string edge in edges)
            {
                string[] edgeArray = edge.Split('-');

                int u = int.Parse(edgeArray[0]);
                int v = int.Parse(edgeArray[1]);

                matrix[u, v] = 1;
                matrix[v, u] = 1;
            }
            return new Graph(matrix);
        }

        else if (graphTypeInput == "Directional" ||
                 graphTypeInput == "d")
        {
            foreach (string edge in edges)
            {
                string[] edgeArray = edge.Split('-');

                int u = int.Parse(edgeArray[0]);
                int v = int.Parse(edgeArray[1]);

                matrix[u, v] = 1;
            }
            return new Graph(matrix, isDirected: true);
        }

        else
        {
            throw new ArgumentException("Invalid string value for command", nameof(graphTypeInput));
        }
    }

    //private static Graph InputMatrix()
    //{
    //}

    private static Graph GenerateGraph()
    {
        Console.WriteLine("Enter graph type (Standard, Directional): ");
        string graphTypeInput = Console.ReadLine();

        Console.WriteLine("Enter number of vertices: ");
        string verticesCountInput = Console.ReadLine();
        int verticesCount = int.Parse(verticesCountInput);
        int[,] matrix = new int[verticesCount, verticesCount];

        Random rnd = new Random();

        if (graphTypeInput == "Standard" ||
            graphTypeInput == "s")
        {
            for (int i = 0; i < verticesCount; i++)
            {
                for (int q = i; q < verticesCount; q++)
                {
                    matrix[i, q] = rnd.Next(2);
                    matrix[q, i] = matrix[i, q];
                }
            }
            return new Graph(matrix);
        }

        else if (graphTypeInput == "Directional" ||
                 graphTypeInput == "d")
        {
            for (int i = 0; i < verticesCount; i++)
            {
                for (int q = 0; q < verticesCount; q++)
                {
                    matrix[i, q] = rnd.Next(2);
                }
            }
            return new Graph(matrix, isDirected: true);
        }

        else
        {
            throw new ArgumentException("Invalid string value for command", nameof(graphTypeInput));
        }
    }
}
