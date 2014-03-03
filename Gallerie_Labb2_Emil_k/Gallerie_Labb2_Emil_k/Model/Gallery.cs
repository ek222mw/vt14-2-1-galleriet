using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
using System.IO;
using System.Drawing;

namespace Gallerie_Labb2_Emil_k.Model
{
    public class Gallery
    {
        //fält
        private static Regex ApprovedExenstions = new Regex(@"(.*?)\.(jpg|jpeg|png|gif)$");
        private static string PhysicalUploadedImagesPath;
        private static Regex SantizePath;
        private static string PhysicalUploadedImagesThumbnailPath;

        //Metoder
        static Gallery()
        {
            PhysicalUploadedImagesPath = Path.Combine(AppDomain.CurrentDomain.GetData("APPBASE").ToString(), "Pictures");
            var invalidChars = new string(Path.GetInvalidFileNameChars());
            SantizePath = new Regex(string.Format("[{0}]", Regex.Escape(invalidChars)));
            PhysicalUploadedImagesThumbnailPath = Path.Combine(AppDomain.CurrentDomain.GetData("APPBASE").ToString(), "Thumbnail");

        }

       public static IEnumerable<string> GetImageNames()
       {
           var fileinfos = new DirectoryInfo(PhysicalUploadedImagesThumbnailPath).GetFiles();
           List<string> imagenames = new List<string>();
           foreach(var fileinfo in fileinfos)
           {
               if(ApprovedExenstions.IsMatch(fileinfo.Extension))
               {
                   imagenames.Add(fileinfo.Name);
               }

           }
           imagenames.TrimExcess();
           imagenames.Sort();
           return imagenames;
        }
        public static bool ImageExists(string name)
        {
            return File.Exists(Path.Combine(PhysicalUploadedImagesPath,name));
        }
        private static bool IsValidImage(Image image)
        {
            return image.RawFormat.Guid == System.Drawing.Imaging.ImageFormat.Gif.Guid ||
                image.RawFormat.Guid == System.Drawing.Imaging.ImageFormat.Jpeg.Guid ||
                image.RawFormat.Guid == System.Drawing.Imaging.ImageFormat.Gif.Guid;
        }
        public static string SaveImage(Stream stream, string fileName)
        {
            var image = System.Drawing.Image.FromStream(stream);

            if (!IsValidImage(image))
            {
                throw new ArgumentException();
            }

            int i = 0;
            if (ImageExists(fileName))
            {
                var extension = Path.GetExtension(fileName);
                var imageName = Path.GetFileNameWithoutExtension(fileName);
                do
                {
                    i++;
                    fileName = String.Format("{0}({1}){2}", imageName, i, extension);

                } while (ImageExists(fileName));
            }

            image.Save(Path.Combine(PhysicalUploadedImagesPath, fileName));
            var thumbnail = image.GetThumbnailImage(60, 45, null, System.IntPtr.Zero);
            thumbnail.Save(Path.Combine(PhysicalUploadedImagesThumbnailPath, fileName));

            return fileName;
        }
    }
}