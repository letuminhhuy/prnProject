using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Store_Management.Data.Models;
using Store_Management.Utils;

namespace Store_Management.Views.EmployeeView.Converter
{

    public class RoleToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Employee.Role role = (Employee.Role)value;
            return role.ToRoleName();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
