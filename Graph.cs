using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphProject
{
    public class Graph(int[][] adjacencyMatrix)
    {
        private int _verticesCount = adjacencyMatrix.GetLength(0);
        private int[][] _adjacencyMatrix = adjacencyMatrix;

        public int VerticesCount()
        {
            return _verticesCount;
        }

        public int[][] AdjacencyMatrix()
        {
            return _adjacencyMatrix;
        }

        public int[] GetNeighbors(int u)
        {
            if (u >= _verticesCount)
            {
                Console.WriteLine("Error: Vertice index out of range (Graph: GetNeightbors)");
                return [];
            }

            int[] neighbors = [];

            for (int i=0; i < _verticesCount; i++) 
            { 
                if (_adjacencyMatrix[u][i] == 1) { neighbors.Append(i); } 
            }
            return neighbors;
        }

        public void RemoveEdge(int u, int v) {
            _adjacencyMatrix[u][v] = 0;
            _adjacencyMatrix[v][u] = 0;
        }

        public bool IsBridge(int u, int v)
        {
            if (_adjacencyMatrix[u][v] == 0) { return false; }
            Graph edgelessGraph = new Graph(_adjacencyMatrix);
            edgelessGraph.RemoveEdge(u, v);
            if (edgelessGraph.VertexComponent(u)[v] == false) { return true; }
            return false;
        }

        private bool[] VertexComponent(int startVertex)
        {
            if (startVertex >= _verticesCount)
            {
                Console.WriteLine("Error: Vertice index out of range (Graph: VertexComponent)");
                return [];
            }

            bool[] uComponent = new bool[_verticesCount];
            VertexComponentInternal(startVertex, uComponent);

            void VertexComponentInternal(int vertex, bool[] visited)
            {
                visited[vertex] = true;
                foreach (int neighbor in GetNeighbors(vertex))
                {
                    if (!visited[neighbor]) { VertexComponentInternal(neighbor, visited); }
                }
            }

            return uComponent;
        }
    }   
}

