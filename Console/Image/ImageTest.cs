using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Console = System.Console;

namespace Console.Image
{
    public class ImageTest
    {
        public static void CreateImage()
        {
            Bitmap bmp = new Bitmap("c:/delete/filename.bmp");

            RectangleF rectf = new RectangleF(70, 90, 90, 50);

            Graphics g = Graphics.FromImage(bmp);

            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.DrawString("yourText", new Font("Tahoma", 8), Brushes.Black, rectf);

            g.Flush();

            bmp.Save("c:/delete/2.bmp");
        }

        public static void TransformImageWithRedFilter()
        {
            var inputImagePath = @"c:\delete\IMG_0244.jpg";
            var outputImagePath = @"c:/delete/11.jpg";
            var ms = new MemoryStream();
            using (var bitmap = new Bitmap(inputImagePath))
            {
                var height = bitmap.Size.Height;
                var width = bitmap.Size.Width;
                System.Console.WriteLine($"size of image {height} x {width}");
                for (var y = 0; y < height; y++)
                {
                    if (y % 2 == 0)
                    {
                        for (int x = 0; x < width; x++)
                        {
                            // set the pixel to the new gray color
                            bitmap.SetPixel(x, y, Color.Red);
                        }
                    }
                }
                bitmap.Save(ms, ImageFormat.Jpeg);
                File.WriteAllBytes("c:/delete/11.jpg", ms.ToArray());
            }
        }

        public static void ResizeImage()
        {
            var inputImagePath = @"c:\delete\IMG_0244.jpg";
            var outputImagePath = @"c:/delete/resized.jpg";
            var defaultWidth = 64;
            var defaultHeight = 64;

            using (var stream = new FileStream(inputImagePath, FileMode.Open, FileAccess.Read))
            {
                using (var fromImage = System.Drawing.Image.FromStream(stream))
                {
                    double ratioW = (double)defaultWidth / (double)fromImage.Size.Width;
                    double ratioH = (double)defaultHeight / (double)fromImage.Size.Height;
                    double ratio = ratioW < ratioH ? ratioW : ratioH;
                    int resizedWidth = (int)(fromImage.Size.Width * ratio);
                    int resizedHeight = (int)(fromImage.Size.Height * ratio);

                    using (var toImage = new Bitmap(resizedWidth, resizedHeight))
                    {
                        using (var graphics = Graphics.FromImage(toImage))
                        {
                            graphics.CompositingMode = CompositingMode.SourceCopy;
                            graphics.CompositingQuality = CompositingQuality.HighQuality;
                            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            graphics.SmoothingMode = SmoothingMode.HighQuality;
                            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                            graphics.DrawImage(fromImage, 0, 0, resizedWidth, resizedHeight);
                            toImage.Save(outputImagePath);
                        }
                    }

                    //var resized = bitmap.GetThumbnailImage(256, 256, null, IntPtr.Zero);
                    //resized.Save(outputImagePath, ImageFormat.Jpeg);
                }
            }
        }
    }
}
