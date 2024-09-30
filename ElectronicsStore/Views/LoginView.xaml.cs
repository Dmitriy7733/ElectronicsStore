using System.Windows;
using System.Windows.Controls;
using ElectronicsStore.ViewModels;

namespace ElectronicsStore.Views
{
    public partial class LoginView : Window
    {
        public LoginViewModel ViewModel { get; set; }

        public LoginView()
        {
            InitializeComponent();
            ViewModel = new LoginViewModel();
            DataContext = ViewModel;
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (sender is PasswordBox passwordBox)
            {
                ViewModel.Password = passwordBox.Password; 
            }
        }
    }
}
