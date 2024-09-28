using ElectronicsStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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
