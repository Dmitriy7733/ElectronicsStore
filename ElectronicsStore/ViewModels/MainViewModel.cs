using ElectronicsStore.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace ElectronicsStore.ViewModels;

public class MainViewModel : INotifyPropertyChanged
{
    private string _selectedRole;
    public string SelectedRole
    {
        get => _selectedRole;
        set
        {
            _selectedRole = value;
            OnPropertyChanged();
            if (value != null)
            {
                ShowLoginView();
            }
        }
    }

    public ICommand LoginCommand { get; }

    public MainViewModel()
    {
        LoginCommand = new RelayCommand(ShowLoginView);
    }

    private void ShowLoginView()
    {
        var loginView = new LoginView();
        loginView.Show();
        Application.Current.MainWindow.Close(); // Закрыть текущее окно
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
