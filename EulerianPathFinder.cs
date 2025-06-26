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
        Stack<int> cycle = new Stack<int>();
        cycle.Push(0);

        while (true) 
        {
            List<int> neighbors = graph.GetNeighbors(cycle.Peek());
            Console.WriteLine($"{cycle.Peek()}: {neighbors.Count()}");
            if (neighbors.Count == 0) { break; }
            bool foundNextVertex = false;

            foreach (int neighbor in neighbors)
            {
                if (graph.IsBridge(cycle.Peek(), neighbor))
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
}

