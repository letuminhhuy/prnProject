using Store_Management.Data.Models;
using Store_Management.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store_Management.ViewModels.SaleVM
{
    public class CreateSaleItemVM : ViewModelBase
    {

        public delegate void CloseDialogHanlder();
        public event CloseDialogHanlder OnCloseDialog;
        public CreateSaleItemVM(Book book) {
            CreateSaleItemCommand = new(async obj => await Create());
            Book = book;
        }

        public SaleItem CreatedSaleItem;

        private int _saleId;
        private Book _book;
        private int _bookId;
        private int _quantity;
     

        #region Properties
        public int SaleId { get => _saleId; set => SetProperty(ref _saleId, value); }
        public int BookId { get => _bookId; set => SetProperty(ref _bookId, value); }
        public int Quantity { get => _quantity; set =>SetProperty(ref _quantity , value); }
        public Book Book { get => _book; set => SetProperty(ref _book , value); }

       
        #endregion


        public RelayCommand CreateSaleItemCommand { get;  }

        private async Task Create()
        {
            if(Quantity > Book.Stock)
            {
                Notification.Error("Invalid quantity!");
                return;
            }
            SaleItem saleItem = new SaleItem
            {
                CreatedBy = StoreSession.Instance.ActiveEmployee.Id,
                CreatedDate = DateTime.Now,
                SaleId = SaleId,
                quantity = Quantity,
                IsActive = true,
                Book = Book,

            };
            this.CreatedSaleItem = saleItem;
            OnCloseDialog.Invoke();

            
        }
    }
}
