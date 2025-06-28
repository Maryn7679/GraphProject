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

        DrawLine((32, 75), (111, 38), bitmap_red);
        DrawLine((111, 38), (32, 75), bitmap_red);
        DrawLine((32, 75), (111, 38), bitmap_red);
        DrawLine((59, 93), (86, 25), bitmap_red);
        DrawLine((59, 25), (86, 93), bitmap_red);

        bitmap_red.Save("Images/red.png");
    }

    //public static void DrawEulerianPath(Stack<int> path, int verticesCount, int canvasSize)
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

    private static void DrawLine((int, int) a, (int, int) b, Bitmap canvas) 
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

                Color color = Color.Magenta;
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

                Color color = Color.Magenta;
                canvas.SetPixel(x, y, color);
            }
        }
    }
}
