using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
public class ImageOptimization
{
    public enum Dimensions
    {
        Width,
        Height
    }
    public enum AnchorPosition
    {
        Top,
        Center,
        Bottom,
        Left,
        Right
    }
    public static void ScaleByPercent(string imageFile, string savePath, string thumbPrefix, string fileName, int Percent)
    {
        System.Drawing.Image imgPhoto = System.Drawing.Image.FromFile(imageFile);
        float nPercent = ((float)Percent / 100);

        int sourceWidth = imgPhoto.Width;
        int sourceHeight = imgPhoto.Height;
        int sourceX = 0;
        int sourceY = 0;

        int destX = 0;
        int destY = 0;
        int destWidth = (int)(sourceWidth * nPercent);
        int destHeight = (int)(sourceHeight * nPercent);

        Bitmap bmPhoto = new Bitmap(destWidth, destHeight, PixelFormat.Format24bppRgb);
        bmPhoto.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);

        Graphics grPhoto = Graphics.FromImage(bmPhoto);
        grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;

        grPhoto.DrawImage(imgPhoto,
            new Rectangle(destX, destY, destWidth, destHeight),
            new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight),
            GraphicsUnit.Pixel);

        grPhoto.Dispose();
        bmPhoto.Save(savePath + thumbPrefix + fileName);
        imgPhoto.Dispose();
        bmPhoto.Dispose();
    }
    public static string ConstrainProportions(string imageFile, string savePath, string thumbPrefix, string fileName, int Size, Dimensions Dimension)
    {
        System.Drawing.Image imgPhoto = System.Drawing.Image.FromFile(imageFile);
        int sourceWidth = imgPhoto.Width;
        int sourceHeight = imgPhoto.Height;
        int sourceX = 0;
        int sourceY = 0;
        int destX = 0;
        int destY = 0;
        float nPercent = 0;

        switch (Dimension)
        {
            case Dimensions.Width:
                nPercent = ((float)Size / (float)sourceWidth);
                break;
            default:
                nPercent = ((float)Size / (float)sourceHeight);
                break;
        }

        int destWidth = (int)(sourceWidth * nPercent);
        int destHeight = (int)(sourceHeight * nPercent);

        Bitmap bmPhoto = new Bitmap(destWidth, destHeight, PixelFormat.Format24bppRgb);
        bmPhoto.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);

        Graphics grPhoto = Graphics.FromImage(bmPhoto);
        grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;

        grPhoto.DrawImage(imgPhoto,
        new Rectangle(destX, destY, destWidth, destHeight),
        new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight),
        GraphicsUnit.Pixel);
        fileName = thumbPrefix + System.IO.Path.GetFileNameWithoutExtension(fileName) + "_" + System.Guid.NewGuid().ToString() + Path.GetExtension(fileName);
        grPhoto.Dispose();
        bmPhoto.Save(savePath + "\\" + fileName);
        imgPhoto.Dispose();
        bmPhoto.Dispose();
        return fileName;
    }
    public static void FixedSize(string imageFile, string savePath, string thumbPrefix, string fileName, int Width, int Height)
    {
        System.Drawing.Image imgPhoto = System.Drawing.Image.FromFile(imageFile);
        int sourceWidth = imgPhoto.Width;
        int sourceHeight = imgPhoto.Height;
        int sourceX = 0;
        int sourceY = 0;
        int destX = 0;
        int destY = 0;

        float nPercent = 0;
        float nPercentW = 0;
        float nPercentH = 0;

        nPercentW = ((float)Width / (float)sourceWidth);
        nPercentH = ((float)Height / (float)sourceHeight);

        //if we have to pad the height pad both the top and the bottom
        //with the difference between the scaled height and the desired height
        if (nPercentH < nPercentW)
        {
            nPercent = nPercentH;
            destX = (int)((Width - (sourceWidth * nPercent)) / 2);
        }
        else
        {
            nPercent = nPercentW;
            destY = (int)((Height - (sourceHeight * nPercent)) / 2);
        }

        int destWidth = (int)(sourceWidth * nPercent);
        int destHeight = (int)(sourceHeight * nPercent);

        Bitmap bmPhoto = new Bitmap(Width, Height, PixelFormat.Format24bppRgb);
        bmPhoto.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);

        Graphics grPhoto = Graphics.FromImage(bmPhoto);
        grPhoto.Clear(Color.Red);
        grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;

        grPhoto.DrawImage(imgPhoto,
            new Rectangle(destX, destY, destWidth, destHeight),
            new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight),
            GraphicsUnit.Pixel);

        grPhoto.Dispose();
        bmPhoto.Save(savePath + thumbPrefix + fileName);
        imgPhoto.Dispose();
        bmPhoto.Dispose();
    }
    public static void Crop(string imageFile, string savePath, string thumbPrefix, string fileName, int Width, int Height, AnchorPosition Anchor)
    {
        System.Drawing.Image imgPhoto = System.Drawing.Image.FromFile(imageFile);
        int sourceWidth = imgPhoto.Width;
        int sourceHeight = imgPhoto.Height;
        int sourceX = 0;
        int sourceY = 0;
        int destX = 0;
        int destY = 0;

        float nPercent = 0;
        float nPercentW = 0;
        float nPercentH = 0;

        nPercentW = ((float)Width / (float)sourceWidth);
        nPercentH = ((float)Height / (float)sourceHeight);

        if (nPercentH < nPercentW)
        {
            nPercent = nPercentW;
            switch (Anchor)
            {
                case AnchorPosition.Top:
                    destY = 0;
                    break;
                case AnchorPosition.Bottom:
                    destY = (int)(Height - (sourceHeight * nPercent));
                    break;
                default:
                    destY = (int)((Height - (sourceHeight * nPercent)) / 2);
                    break;
            }
        }
        else
        {
            nPercent = nPercentH;
            switch (Anchor)
            {
                case AnchorPosition.Left:
                    destX = 0;
                    break;
                case AnchorPosition.Right:
                    destX = (int)(Width - (sourceWidth * nPercent));
                    break;
                default:
                    destX = (int)((Width - (sourceWidth * nPercent)) / 2);
                    break;
            }
        }

        int destWidth = (int)(sourceWidth * nPercent);
        int destHeight = (int)(sourceHeight * nPercent);

        Bitmap bmPhoto = new Bitmap(Width, Height, PixelFormat.Format24bppRgb);
        bmPhoto.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);

        Graphics grPhoto = Graphics.FromImage(bmPhoto);
        grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;

        grPhoto.DrawImage(imgPhoto,
            new Rectangle(destX, destY, destWidth, destHeight),
            new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight),
            GraphicsUnit.Pixel);

        grPhoto.Dispose();
        bmPhoto.Save(savePath + thumbPrefix + fileName);
        imgPhoto.Dispose();
        bmPhoto.Dispose();
    }
}


