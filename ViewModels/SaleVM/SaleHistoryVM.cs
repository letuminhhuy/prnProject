using Store_Management.Data.Models;
using Store_Management.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store_Management.ViewModels.SaleVM
{
    public class SaleHistoryVM : ViewModelBase
    {
        SaleService SaleService { get; set; } = new SaleService();

        public SaleHistoryVM() {
            Init();
        
        }
        private async void Init()
        {
          Sales = new ObservableCollection<Sale> (await SaleService.GetAll());
        }
        private ObservableCollection<Sale> _sales;
        public ObservableCollection<Sale> Sales { get => _sales; set => SetProperty(ref _sales, value); }
    }
}
