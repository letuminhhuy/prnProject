using Store_Management.Data.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store_Management.ViewModels.PublisherVM
{
    public class PublisherSelectVM : ViewModelBase
    {
        public PublisherSelectVM() { }

        private ObservableCollection<Publisher> _publisherList;

        public ObservableCollection<Publisher> PublisherList
        {
            get { return _publisherList; }
            set { _publisherList = value; OnPropertyChanged(); }
        }
    }
}
