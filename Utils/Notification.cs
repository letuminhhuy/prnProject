using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Store_Management.Utils
{
    public class Notification
    {

        public static void Error(string message, string? caption = "Error")
        {
            MessageBox.Show(message, caption, MessageBoxButton.OK);   
        }
        public static void Info(string message, string? caption = "Info")
        {
            MessageBox.Show(message, caption, MessageBoxButton.OK);   
        } 
        public static void Success(string message, string? caption = "Success")
        {
            MessageBox.Show(message, caption, MessageBoxButton.OK);   
        }
    }
}
