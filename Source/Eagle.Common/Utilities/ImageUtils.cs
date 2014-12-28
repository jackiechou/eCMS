using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace Eagle.Common.Utilities
{
    public static class ImageUtils
    {
        public static string CreateWatermark(string virtual_background_image_path, string virtual_watermark_path, string final_image_path)
        {           
            Image image = System.Drawing.Image.FromFile(HttpContext.Current.Server.MapPath(virtual_background_image_path));//This is the background image
            Image logo = Image.FromFile(HttpContext.Current.Server.MapPath(virtual_watermark_path)); //This is your watermark
            Graphics g = System.Drawing.Graphics.FromImage(image); //Create graphics object of the background image //So that you can draw your logo on it
            Bitmap TransparentLogo = new Bitmap(logo.Width, logo.Height); //Create a blank bitmap object //to which we //draw our transparent logo
            Graphics TGraphics = Graphics.FromImage(TransparentLogo);//Create a graphics object so that //we can draw //on the blank bitmap image object
            ColorMatrix ColorMatrix = new ColorMatrix(); //An image is represenred as a 5X4 matrix(i.e 4 //columns and 5 //rows)
            ColorMatrix.Matrix33 = 0.25F;//the 3rd element of the 4th row represents the transparency
            ImageAttributes ImgAttributes = new ImageAttributes();//an ImageAttributes object is used to set all //the alpha //values.This is done by initializing a color matrix and setting the alpha scaling value in the matrix.The address of //the color matrix is passed to the SetColorMatrix method of the //ImageAttributes object, and the //ImageAttributes object is passed to the DrawImage method of the Graphics object.
            ImgAttributes.SetColorMatrix(ColorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap); TGraphics.DrawImage(logo, new Rectangle(0, 0, TransparentLogo.Width, TransparentLogo.Height), 0, 0, TransparentLogo.Width, TransparentLogo.Height, GraphicsUnit.Pixel, ImgAttributes);
            TGraphics.Dispose();
            g.DrawImage(TransparentLogo, image.Width / 2, 30);

            ImageFormat _ImageFormat = null;
            string physical_final_file_path = HttpContext.Current.Server.MapPath(final_image_path);
            string image_name = Path.GetFileName(physical_final_file_path);
            string final_image_extension = Path.GetExtension(physical_final_file_path);
            switch (final_image_extension.ToLower())
            {
                case ".jpg":
                    _ImageFormat = ImageFormat.Jpeg;
                    break;
                case ".jpeg":
                    _ImageFormat = ImageFormat.Jpeg;
                    break;
                case ".png":
                    _ImageFormat = ImageFormat.Png;
                    break;
                case ".gif":
                    _ImageFormat = ImageFormat.Gif;
                    break;
                case ".bmp":
                    _ImageFormat = ImageFormat.Bmp;
                    break;
                default:
                    _ImageFormat = ImageFormat.Jpeg;
                    break;
            }

            image.Save(physical_final_file_path, _ImageFormat);
            return image_name;
        }

        public static Color[] GetUniqueRandomColor(int count)
        {
            Color[] colors = new Color[count];
            Dictionary<Color, bool> hs = new Dictionary<Color, bool>();

            Random randomColor = new Random();

            for (int i = 0; i < count; i++)
            {
                Color color;
                while (hs.ContainsKey(color = Color.FromArgb(randomColor.Next(70, 200), randomColor.Next(100, 225), randomColor.Next(100, 230)))) ;
                hs.Add(color, true);
                colors[i] = color;
            }

            return colors;
        }

        public static string[] GetUniqueRandomShapeStrings(int theSize)
        {
            string[] basic_shapes = new string[5] { "circle", "diamond", "square", "triangle", "cross" };
            List<string> shapeList = new List<string>();
            int basic_lst_total = basic_shapes.Length;
            if (theSize <= basic_lst_total)
            {
                for (int i = 0; i < theSize; i++)
                    shapeList.Add(basic_shapes[i]);
            }
            else
            {
                foreach (var item in basic_shapes)
                    shapeList.Add(item);
                while (basic_lst_total < theSize)
                {
                    foreach (var k in basic_shapes)
                    {
                        if (basic_lst_total < theSize)
                        {
                            shapeList.Add(k);
                            basic_lst_total++;
                        }
                    }
                }
            }
            return shapeList.ToArray();
        }


        //public static string[] GetUniqueRandomShapeStrings(int count)
        //{
        //    string[] basic_shapes = { "circle", "diamond", "square", "triangle", "cross" };
        //    int basic_array_total = basic_shapes.Count();
        //    string[] shapes = new string[count];
        //    if (count <= basic_array_total)
        //    {
        //        for (int x = 0; x < shapes.Length; x++)
        //        {
        //            shapes[x] = basic_shapes[x];
        //        }
        //    }
        //    else
        //    {

        //    }

        //    string[] shapes = new string[5];
        //    for (int x = 0; x < shapes.Length; x++) 
        //    {
                
        //        shapes[x] = basic_shapes[x];
        //    }

        //    List<string> lst = new List<string>() { "circle", "diamond", "square", "triangle", "cross" };
        //    foreach (var split in lst)
        //    {
        //        features.Add(split[0]);
        //        projects.Add(split[1]);
        //    }



        //    return shapes;
        //}

        public static string[] GetUniqueRandomHexStrings(int count)
        {
            string[] colors = new string[count];
            Dictionary<Color, bool> dict = new Dictionary<Color, bool>();

            Random randomColor = new Random();

            for (int i = 0; i < count; i++)
            {
                Color color;
                while (dict.ContainsKey(color = Color.FromArgb(randomColor.Next(70, 200), randomColor.Next(100, 225), randomColor.Next(100, 230)))) ;
                dict.Add(color, true);
                //colors[i] = color;
                colors[i] = System.Drawing.ColorTranslator.ToHtml(color);
            }
            return colors;
        }

        public static List<string> GetUniqueRandomHexList(int count)
        {
            string[] colors = new string[count];
            Dictionary<Color, bool> dict = new Dictionary<Color, bool>();

            Random randomColor = new Random();

            for (int i = 0; i < count; i++)
            {
                Color color;
                while (dict.ContainsKey(color = Color.FromArgb(randomColor.Next(70, 200), randomColor.Next(100, 225), randomColor.Next(100, 230)))) ;
                dict.Add(color, true);
                //colors[i] = color;
                colors[i] = System.Drawing.ColorTranslator.ToHtml(color);
            }
            return colors.ToList();
        }

        public static string ColorToHexString(Color color)
        {
            char[] hexDigits = {
         '0', '1', '2', '3', '4', '5', '6', '7',
         '8', '9', 'A', 'B', 'C', 'D', 'E', 'F'};

            byte[] bytes = new byte[3];
            bytes[0] = color.R;
            bytes[1] = color.G;
            bytes[2] = color.B;
            char[] chars = new char[bytes.Length * 2];
            for (int i = 0; i < bytes.Length; i++)
            {
                int b = bytes[i];
                chars[i * 2] = hexDigits[b >> 4];
                chars[i * 2 + 1] = hexDigits[b & 0xF];
            }
            return new string(chars);
        }


        public static void CreateFileFromByteArray(string FilePath, byte[] FileContent)
        {
            if (!System.IO.File.Exists(FilePath))
            {
                FileStream fs = new FileStream(FilePath, FileMode.Create, FileAccess.Write);
                fs.Write(FileContent, 0, FileContent.Length);
                fs.Close();
            }
        }

        public static void CreateFileFromByteArray(string FileNameWithExtension, string VirtualDirPath, byte[] FileContent)
        {
            string PhysicalDirPath = HttpContext.Current.Server.MapPath(VirtualDirPath);
            if (!Directory.Exists(PhysicalDirPath))
                Directory.CreateDirectory(PhysicalDirPath);

            string FilePath = Path.Combine(PhysicalDirPath, FileNameWithExtension);
            if (!System.IO.File.Exists(FilePath))
            {
                FileStream fs = new FileStream(FilePath, FileMode.Create, FileAccess.Write);
                fs.Write(FileContent, 0, FileContent.Length);
                fs.Close();
            }
        }


        public static void ResizeStream(int maxWidth, int maxHeight, Stream filePath, string outputPath)
        {
            var image = System.Drawing.Image.FromStream(filePath);

            var ratioX = (double)maxWidth / image.Width;
            var ratioY = (double)maxHeight / image.Height;
            var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);


            var thumbnailBitmap = new Bitmap(newWidth, newHeight);

            var thumbnailGraph = Graphics.FromImage(thumbnailBitmap);
            thumbnailGraph.CompositingQuality = CompositingQuality.HighQuality;
            thumbnailGraph.SmoothingMode = SmoothingMode.HighQuality;
            thumbnailGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;

            var imageRectangle = new Rectangle(0, 0, newWidth, newHeight);
            thumbnailGraph.DrawImage(image, imageRectangle);

            thumbnailBitmap.Save(outputPath, image.RawFormat);
            thumbnailGraph.Dispose();
            thumbnailBitmap.Dispose();
            image.Dispose();
        }
    }
}
