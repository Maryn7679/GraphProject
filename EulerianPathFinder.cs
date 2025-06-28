using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphProject;

public class EulerianPathFinder
{
    public static Stack<int> GetEulerianCycle(Graph graph)
    {
        if (!IsEulerian(graph)) 
        {
            throw new ArgumentException("Graph is not Eulerian");
        }

        Stack<int> cycle = new Stack<int>();
        cycle.Push(0);

        while (true) 
        {
            List<int> neighbors = graph.GetNeighbors(cycle.Peek());
            if (neighbors.Count == 0) { break; }
            bool foundNextVertex = false;

            foreach (int neighbor in neighbors)
            {
                if (!graph.IsBridge(cycle.Peek(), neighbor))
                {
                    graph.RemoveEdge(cycle.Peek(), neighbor);
                    cycle.Push(neighbor);
                    foundNextVertex = true;
                    break; 
                }
            }

            if (!foundNextVertex)
            {
                graph.RemoveEdge(cycle.Peek(), neighbors[0]);
                cycle.Push(neighbors[0]);
            }
        }

        return cycle;
    }

    public static bool IsEulerian(Graph graph) 
    {
        if (!graph.IsConnected()) { return false; }

        int[,] matrix = graph.AdjacencyMatrix();
        for (int i = 0; i < graph.VerticesCount(); i++)
        {
            int inDegree = 0;
            int outDegree = 0;

            for (int q = 0; q < graph.VerticesCount(); q++)
            {
                outDegree += matrix[i, q];
                inDegree += matrix[q, i];
            }

            if (inDegree != outDegree) { return false; }
        }

        return true;
    }
}

