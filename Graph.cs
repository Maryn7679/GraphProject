using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GraphProject;

public class Graph(int[,] adjacencyMatrix, bool isDirected = false)
{
    private int _verticesCount = adjacencyMatrix.GetLength(0);
    private int[,] _adjacencyMatrix = (int[,])adjacencyMatrix.Clone();
    private bool _isDirected = isDirected;


    public int VerticesCount()
    {
        return _verticesCount;
    }

    public int[,] AdjacencyMatrix()
    {
        return _adjacencyMatrix;
    }

    public List<int> GetNeighbors(int vertex)
    {
        if (vertex >= _verticesCount)
        {
            throw new ArgumentException("Error: Vertice index out of range (Graph: GetNeightbors)");
        }

        List<int> neighbors = new List<int>();

        for (int i = 0; i < _verticesCount; i++)
        { 
            if (_adjacencyMatrix[vertex, i] == 1) { neighbors.Add(i); } 
        }
        return neighbors;
    }

    public void RemoveEdge(int u, int v) {
        _adjacencyMatrix[u, v] = 0;
        if (!_isDirected)
        {
            _adjacencyMatrix[v, u] = 0;
        }
    }

    public bool IsBridge(int u, int v)
    {
        if (_adjacencyMatrix[u, v] == 0) { return false; }
        Graph edgelessGraph = new Graph(_adjacencyMatrix);
        edgelessGraph.RemoveEdge(u, v);
        if (edgelessGraph.VertexComponent(u)[v] == false) { return true; }
        return false;
    }

    public bool IsConnected()
    {
        bool[] component = VertexComponent(0);
        if (component.All(vertex => vertex == true)) { return true; }
        return false;
    }

    private bool[] VertexComponent(int startVertex)
    {
        if (startVertex >= _verticesCount)
        {
            throw new ArgumentException("Error: Vertice index out of range (Graph: VertexComponent)");
        }

        bool[] startComponent = new bool[_verticesCount];
        VertexComponentInternal(startVertex, startComponent);

        void VertexComponentInternal(int vertex, bool[] visited)
        {
            visited[vertex] = true;
            foreach (int neighbor in GetNeighbors(vertex))
            {
                if (!visited[neighbor]) { VertexComponentInternal(neighbor, visited); }
            }
        }

        return startComponent;
    }
}   
