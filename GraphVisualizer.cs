using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace GraphProject;

public class GraphVisualizer
{
    public static void DrawEulerianPath(Stack<int> path, HashSet<int> dominationSet, int verticesCount, int canvasSize)
    {
        Bitmap bitmap = new Bitmap(canvasSize, canvasSize);
        (int, int)[] vertexCoordinates = new (int, int)[verticesCount];
        Random random = new Random();
        ColorGradient palette = new ColorGradient(Color.Yellow, Color.Magenta, path.Count - 1);

        for (int i = 0; i < verticesCount; i++)
        {
            int x = random.Next(10, canvasSize - 10);
            int y = random.Next(10, canvasSize - 10);

            vertexCoordinates[i] = (x, y);
        }

        int origin = path.Pop();
        int startVertex = origin;
        foreach (int nextVertex in path) 
        {
            Color nextColor = palette.Next();
            DrawLine(vertexCoordinates[startVertex], vertexCoordinates[nextVertex], nextColor, bitmap);
            if (dominationSet.Contains(nextVertex))
            {
                DrawPoint(vertexCoordinates[nextVertex], Color.Lime, bitmap);
            }
            else
            {
                DrawPoint(vertexCoordinates[nextVertex], nextColor, bitmap);
            }

            startVertex = nextVertex;
        }
        Color lastColor = palette.Next();
        DrawLine(vertexCoordinates[startVertex], vertexCoordinates[origin], lastColor, bitmap);
        if (dominationSet.Contains(origin))
        {
            DrawPoint(vertexCoordinates[origin], Color.Lime, bitmap);
        }
        else
        {
            DrawPoint(vertexCoordinates[origin], lastColor, bitmap);
        }

        bitmap.Save("Images/Graph.png");
    }

    private static void DrawLine((int, int) a, (int, int) b, Color color, Bitmap canvas) 
    {
        int xDistance = Math.Abs(a.Item1 - b.Item1);
        int yDistance = Math.Abs(a.Item2 - b.Item2);
        int maxDistance;
        bool maxDistanceIsX;

        if (xDistance >= yDistance)
        {
            maxDistance = xDistance;
            maxDistanceIsX = true;
        }
        else
        {
            maxDistance = yDistance;
            maxDistanceIsX = false;
        }

        if ((a.Item1 - b.Item1)*(a.Item2 - b.Item2) >= 0)
        {
            for (int i = 0; i < maxDistance; i++)
            {
                int x; int y;
                if (maxDistanceIsX)
                {
                    x = i + Math.Min(a.Item1, b.Item1);
                    y = Math.Min(a.Item2, b.Item2) + ((i - 1) * yDistance / (maxDistance - 2));
                }
                else
                {
                    x = Math.Min(a.Item1, b.Item1) + ((i - 1) * xDistance / (maxDistance - 2));
                    y = i + Math.Min(a.Item2, b.Item2);
                }

                canvas.SetPixel(x, y, color);
            }
        }
        else
        {
            for (int i = maxDistance; i > 0; i--)
            {
                int x; int y;
                if (maxDistanceIsX)
                {
                    x = Math.Max(a.Item1, b.Item1) - i;
                    y = Math.Min(a.Item2, b.Item2) + ((i - 1) * yDistance / (maxDistance - 2));
                }
                else
                {
                    x = Math.Min(a.Item1, b.Item1) + ((i - 1) * xDistance / (maxDistance - 2));
                    y = Math.Max(a.Item2, b.Item2) - i;
                }

                canvas.SetPixel(x, y, color);
            }
        }
    }

    private static void DrawPoint((int, int) point, Color color, Bitmap canvas)
    {
        for (int x = point.Item1 - 2; x <= point.Item1 + 2; x++)
        {
            for (int y = point.Item2 - 2; y <= point.Item2 + 2; y++)
            {
                if (Math.Abs(point.Item1 - x) + Math.Abs(point.Item2 - y) < 4)
                {
                    canvas.SetPixel(x, y, color);
                }
            }
        }
    }
}

public class ColorGradient(Color startColor, Color endColor, int steps)
{
    private Color _lastColor = startColor;
    private Color _nextColor = startColor;
    private int _step = 0;

    private int redChange = (endColor.R - startColor.R) / (steps - 1);
    private int greenChange = (endColor.G - startColor.G) / (steps - 1);
    private int blueChange = (endColor.B - startColor.B) / (steps - 1);

    public Color Next()
    {
        _lastColor = _nextColor;
        _nextColor = GetNextColor();
        return _lastColor;
    }

    private Color GetNextColor()
    {
        _step += 1;
        if (_step == steps) 
        {
            redChange = 0; greenChange = 0; blueChange = 0;
        }

        Color next = Color.FromArgb(_lastColor.R + redChange,
                                    _lastColor.G + greenChange,
                                    _lastColor.B + blueChange);
        return next;
    }
}
