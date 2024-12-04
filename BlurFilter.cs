using System;
using System.Drawing;
using System.Drawing.Imaging;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Image Processing Menu");
        Console.WriteLine("1. Rotate Image");
        Console.WriteLine("2. Blur Image");
        Console.WriteLine("3. Rotate and Blur Image");
        Console.Write("Please select an option (1-3): ");
        
        string option = Console.ReadLine();
        Console.Write("Enter the path to the image: ");
        string inputImagePath = Console.ReadLine();
        string outputImagePath = "output_image.jpg";

        switch (option)
        {
            case "1":
                Console.Write("Enter rotation angle (in degrees): ");
                float rotationAngle = float.Parse(Console.ReadLine());
                using (Bitmap originalImage = new Bitmap(inputImagePath))
                {
                    Bitmap rotatedImage = RotateImage(originalImage, rotationAngle);
                    rotatedImage.Save(outputImagePath, ImageFormat.Jpeg);
                    Console.WriteLine("Image rotated and saved as " + outputImagePath);
                }
                break;

            case "2":
                using (Bitmap originalImage = new Bitmap(inputImagePath))
                {
                    Bitmap blurredImage = BlurImage(originalImage);
                    blurredImage.Save(outputImagePath, ImageFormat.Jpeg);
                    Console.WriteLine("Image blurred and saved as " + outputImagePath);
                }
                break;

            case "3":
                Console.Write("Enter rotation angle (in degrees): ");
                rotationAngle = float.Parse(Console.ReadLine());
                using (Bitmap originalImage = new Bitmap(inputImagePath))
                {
                    Bitmap rotatedImage = RotateImage(originalImage, rotationAngle);
                    Bitmap blurredImage = BlurImage(rotatedImage);
                    blurredImage.Save(outputImagePath, ImageFormat.Jpeg);
                    Console.WriteLine("Image rotated and blurred, saved as " + outputImagePath);
                }
                break;

            default:
                Console.WriteLine("Invalid option selected.");
                break;
        }
    }

    static Bitmap RotateImage(Bitmap image, float angle)
    {
        Bitmap rotatedBitmap = new Bitmap(image.Width, image.Height);
        rotatedBitmap.SetResolution(image.HorizontalResolution, image.VerticalResolution);

        using (Graphics g = Graphics.FromImage(rotatedBitmap))
        {
            g.TranslateTransform((float)image.Width / 2, (float)image.Height / 2);
            g.RotateTransform(angle);
            g.TranslateTransform(-(float)image.Width / 2, -(float)image.Height / 2);
            g.DrawImage(image, new Point(0, 0));
        }

        return rotatedBitmap;
    }

    static Bitmap BlurImage(Bitmap image)
    {
        Bitmap blurredBitmap = new Bitmap(image.Width, image.Height);
        using (Graphics g = Graphics.FromImage(blurredBitmap))
        {
            using (ImageAttributes attributes = new ImageAttributes())
            {
                g.DrawImage(image, new Rectangle(0, 0, image.Width, image.Height));
            }
        }

        return blurredBitmap;
    }
}
