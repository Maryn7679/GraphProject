using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace GraphProject;

public class GraphVisualizer
{
    public static void Draw()
    {
        //var bitmap = new Bitmap(640, 480);
        Image image = Bitmap.FromFile("Images/base.png");
        Bitmap bitmap_red = new Bitmap(image);
        ColorGradient palette = new ColorGradient(Color.Magenta, Color.Cyan, 5);

        DrawLine((31, 75), (110, 38), palette.Next(), bitmap_red);
        DrawLine((111, 38), (32, 75), palette.Next(), bitmap_red);
        DrawLine((33, 75), (112, 38), palette.Next(), bitmap_red);
        DrawLine((59, 93), (86, 25), palette.Next(), bitmap_red);
        DrawLine((59, 25), (86, 93), palette.Next(), bitmap_red);

        bitmap_red.Save("Images/red.png");
    }

    public static void DrawGraph(Stack<int> path, HashSet<int> dominationSet, int verticesCount, int canvasSize)
    {
        Bitmap bitmap = new Bitmap(canvasSize, canvasSize);
        (int, int)[] vertexCoordinates = new (int, int)[verticesCount];
        Random random = new Random();
        ColorGradient palette = new ColorGradient(Color.Lime, Color.Magenta, path.Count - 1);

        for (int i = 0; i < verticesCount; i++)
        {
            int x = random.Next(10, canvasSize - 10);
            int y = random.Next(10, canvasSize - 10);

            vertexCoordinates[i] = (x, y);
        }

        int startVertex = path.Pop();
        foreach (int nextVertex in path) 
        {
            Color nextColor = palette.Next();
            DrawLine(vertexCoordinates[startVertex], vertexCoordinates[nextVertex], nextColor, bitmap);

            if (dominationSet.Contains(nextVertex))
            {
                DrawPoint(vertexCoordinates[nextVertex], Color.Black, bitmap);
            }
            else
            {
                DrawPoint(vertexCoordinates[nextVertex], nextColor, bitmap);
            }

            startVertex = nextVertex;
        }

        bitmap.Save("Images/Eulerian_path.png");
    }

    //public static void DrawGraph(Stack<int> path, int verticesCount, int canvasSize)
    //{
    //    Bitmap bitmap = new Bitmap(canvasSize, canvasSize);
    //    int[][] angles = new int[verticesCount][];
    //    (int, int)[] vertexCoordinates = new (int, int)[verticesCount];
    //    Random random = new Random();

    //    for (int i = 0; i < verticesCount; i++)
    //    {
    //        int x = random.Next(canvasSize);
    //        int y = random.Next(canvasSize);
    //        if (i != 0)
    //        {
    //            int[] newAngles = new int[i];
    //            for (int q = 0; q < i; q++) 
    //            {
    //                int angle = CalculateAngle(vertexCoordinates[q], (x, y));
    //                newAngles[q] = angle;
    //            }
    //foreach ((int, int) existingVertex in vertexCoordinates[..(i - 1)]) 
    //{ 
    //    int angle = CalculateAngle(existingVertex, (x, y));
    //    newAngles.Append(angle);
    //}
    //    }
    //}
    //bitmap_red.Save("Images/red.png");

    //}

    //private static int CalculateAngle((int, int) a, (int, int) b) { }

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
