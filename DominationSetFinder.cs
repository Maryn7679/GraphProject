using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphProject;

public class DominationSetFinder
{
    public static HashSet<int> GetMinimalDominationSet(Graph graph)
    {
        HashSet<int> dominationSet = new HashSet<int>();
        HashSet<int> dominationSetNeighbors = new HashSet<int>();
        dominationSet.Add(0);

        for (int vertex = 0; vertex < graph.VerticesCount(); vertex++)
        {
            if (dominationSet.Contains(vertex))
            {
                foreach (int neighbor in graph.GetNeighbors(vertex))
                {
                    dominationSetNeighbors.Add(neighbor);
                }
                continue;
            }

            if (dominationSetNeighbors.Contains(vertex))
            {
                continue;
            }

            dominationSet.Add(vertex);
        }

        return dominationSet;
    }
}

