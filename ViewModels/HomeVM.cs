using LiveCharts;
using LiveCharts.Wpf;
using Store_Management.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Store_Management.ViewModels
{
    public class HomeVM : ViewModelBase
    {
        //Dependency
        private BookService BookService { get; set; } = new BookService();
        private EmployeeService EmployeeService { get; set; } = new EmployeeService();
        private SaleService SaleService { get; set; } = new SaleService();

        //
        private int _totalBooks;
        private int _totalEmployees;
        private int _totalSales;
        private decimal _totalRevenue;
        private string _currentMonthDisplay;


        //Chart
        private ObservableCollection<string> _chartLabels;
        private SeriesCollection _revenueSeries;


        #region Properties
        public int TotalBooks { get => _totalBooks; set => SetProperty(ref _totalBooks, value); }
        public int TotalEmployees { get => _totalEmployees; set => SetProperty(ref _totalEmployees, value); }
        public decimal TotalRevenue { get => _totalRevenue; set => SetProperty(ref _totalRevenue, value); }
        public int TotalSales { get => _totalSales; set => SetProperty(ref _totalSales, value); }
        public string CurrentMonthDisplay { get => _currentMonthDisplay; set => SetProperty(ref _currentMonthDisplay, value); }
        public ObservableCollection<string> ChartLabels { get => _chartLabels; set => SetProperty(ref _chartLabels, value); }
        public SeriesCollection RevenueSeries { get => _revenueSeries; set => SetProperty(ref _revenueSeries, value); }

        #endregion

        public HomeVM()
        {
            Init();
        }

        public async void Init()
        {
            TotalBooks = await BookService.CountAll();
            TotalEmployees = await EmployeeService.CountActive();
            TotalSales = await SaleService.CountAllOrder();
            TotalRevenue = await SaleService.GetTotalSales();

            CurrentMonthDisplay = DateTime.Now.ToString("MMMM");
            await GetChartData();
        }


        public async Task GetChartData()
        {
            ChartLabels = new ObservableCollection<string>();
            int month = DateTime.Now.Month;
            int year = DateTime.Now.Year;
            var salesData = await SaleService.GetSalesStatisticsForMonthAsync(month, year);
            var chartValues = new ChartValues<decimal>();
            for (int i = 1; i <= DateTime.DaysInMonth(year, month); i++)
            {
                ChartLabels.Add(i.ToString());
                chartValues.Add(salesData.GetValueOrDefault(i));
            }


            RevenueSeries =
            [
                new ColumnSeries
                {
                    Title="Revenue",
                    Values = chartValues,
                }
,
            ];
        }
    }
}
