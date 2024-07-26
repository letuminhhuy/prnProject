using Store_Management.Utils.Constants;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Store_Management.Services
{
    public class ImageService
    {
        public static string PROFILE_PATH = "profile";
        public BitmapImage LoadImage(string filePath)
        {
            string imagePath = GetAbsoluteImagePath(filePath);

            if (File.Exists(imagePath))
            {
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(imagePath);
                bitmap.EndInit();
                return bitmap;
            }

            return null;
        }
        public void SaveImage(BitmapImage bitmapImage, string fileName, string folderPath)
        {
            string imagePath = GetAbsoluteImagePath(fileName, folderPath);

            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                BitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bitmapImage));
                encoder.Save(fileStream);
            }
        }

        public string Upload(string sourceFilePath, string relativeDestinationPath, string fileName)
        {
            try
            {

                return CopyFileToAppData(sourceFilePath, relativeDestinationPath, fileName);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw new Exception("Error upload file!");

            }
        }
        public string GetAppDataPath()
        {
            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string appFolderPath = Path.Combine(appDataPath, StoreData.STORE_NAME.Replace(' ', '-'));

            if (!Directory.Exists(appFolderPath))
            {
                Directory.CreateDirectory(appFolderPath);
            }

            return appFolderPath;
        }
        private string CopyFileToAppData(string sourceFilePath, string relativeDestinationPath, string fileName)
        {
            try
            {
                string destinationPath = GetAbsoluteImagePath(relativeDestinationPath);
                if(!Directory.Exists(destinationPath))
                {
                    Directory.CreateDirectory(destinationPath);
                }
                string destinationFileName = Path.Combine(destinationPath, fileName);
              
                File.Copy(sourceFilePath, destinationFileName, true); // Overwrite if the file already exists
                return destinationFileName;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error copying file: {ex.Message}");
                return null;

            }
        }
        public string GetAbsoluteImagePath(string fileName, string folderPath)
        {
            return Path.Combine(GetAppDataPath(), folderPath, fileName);
        }

        public string GetAbsoluteImagePath(string filePath)
        {
            return Path.Combine(GetAppDataPath(), filePath);
        }
    }
}
