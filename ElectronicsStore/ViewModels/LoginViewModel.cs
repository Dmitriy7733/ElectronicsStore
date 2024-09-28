using ElectronicsStore.Models;
using ElectronicsStore.Services;
using ElectronicsStore.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ElectronicsStore.ViewModels;

public class LoginViewModel : INotifyPropertyChanged
{
    private string _username;
    private string _password;
    private readonly AuthenticationService _authService;

    public LoginViewModel()
    {
        _authService = new AuthenticationService();
        LoginCommand = new RelayCommand(Login);
    }

    public string Username
    {
        get => _username;
        set
        {
            _username = value;
            OnPropertyChanged(nameof(Username));
        }
    }

    public string Password
    {
        get => _password;
        set
        {
            _password = value;
            OnPropertyChanged(nameof(Password));
        }
    }

    public RelayCommand LoginCommand { get; }

    private void Login()
    {
        var user = _authService.Authenticate(Username, Password);
        if (user != null)
        {
            Window newView = null;
            switch (user.Role)
            {
                case UserRole.Admin:
                    newView = new AdminView();
                    break;
                case UserRole.Manager:
                    newView = new ManagerView();

                    break;
                case UserRole.Buyer:
                    newView = new BuyerView();
                    break;
            }
            if (newView != null)
            {
                newView.Show();
                Application.Current.MainWindow.Close(); // Закрываем текущее окно
            }
       
        }
        else
        {
            // Обработка недействительных учетных данных
            MessageBox.Show("Пароль или логин не действительны");
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
