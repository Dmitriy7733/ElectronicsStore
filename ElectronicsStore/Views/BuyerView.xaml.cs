using ElectronicsStore.ViewModels;
using System.Windows;

namespace ElectronicsStore.Views
{
    public partial class BuyerView : Window
    {
        public BuyerView()
        {
            InitializeComponent();
            BuyerViewModel buyerViewModel = new BuyerViewModel();
            DataContext = buyerViewModel; // Установка DataContext
        }

    }
}
