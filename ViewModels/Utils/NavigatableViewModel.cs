using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store_Management.ViewModels.Utils
{
    public class NavigatableViewModel : ViewModelBase
    {
        private ViewModelBase _currentPage = null!;
        public ViewModelBase CurrentPage { get => _currentPage; set => SetProperty(ref _currentPage, value); }
    }
}
