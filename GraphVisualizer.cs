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
        Bitmap bitmap_green = new Bitmap(image);
        Bitmap bitmap_blue = new Bitmap(image);


        for (int x = 0; x < bitmap_red.Width; x++)
        {
            for (int y = 0; y < bitmap_red.Height; y++)
            {
                Color color = Color.FromArgb(bitmap_red.GetPixel(x, y).R, 0, 0);
                Color color2 = Color.FromArgb(0, bitmap_red.GetPixel(x, y).G, 0);
                Color color3 = Color.FromArgb(0, 0, bitmap_red.GetPixel(x, y).B);
                bitmap_red.SetPixel(x, y, color);
                bitmap_green.SetPixel(x, y, color2);
                bitmap_blue.SetPixel(x, y, color3);
            }
        }

        bitmap_red.Save("Images/red.png");
        bitmap_green.Save("Images/green.png");
        bitmap_blue.Save("Images/blue.png");

    }
}
