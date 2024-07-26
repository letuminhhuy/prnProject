using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Store_Management.Utils
{
    public class DialogUtil
    {

        public static string? OpenImagePicker()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.png;*.jpg)|*.png;*.jpg|All Files (*.*)|*.*"; 

            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;

                return filePath;
            }
            return null;
        }
    }
}
