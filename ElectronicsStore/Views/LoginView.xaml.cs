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
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        public LoginViewModel ViewModel { get; set; }
        public LoginView()
        {
            InitializeComponent();
            ViewModel = new LoginViewModel();
            DataContext = ViewModel;
        }

        //public string Password { get; private set; }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            ViewModel.Password = PasswordBox.Password;
        }
    }
}
