using System.Windows.Input;
using NutriTrack.Helpers;
using NutriTrack.Models;
using NutriTrack.Services;

namespace NutriTrack.ViewModels;

public class LoginViewModel : BaseViewModel
{
    private readonly IAuthService _authService;
    private string _username;
    private string _password;
    private string _errorMessage;

    public string Username { get => _username; set => SetProperty(ref _username, value); }
    public string Password { get => _password; set => SetProperty(ref _password, value); }
    public string ErrorMessage { get => _errorMessage; set => SetProperty(ref _errorMessage, value); }

    public ICommand LoginCommand { get; }
    public ICommand ShowRegisterCommand { get; }

    public LoginViewModel(IAuthService authService)
    {
        _authService = authService;
        LoginCommand = new RelayCommand(async _ => await Login());
        ShowRegisterCommand = new RelayCommand(_ => OnShowRegister?.Invoke());
    }

    private async Task Login()
    {
        var user = await _authService.LoginAsync(Username, Password);
        if (user != null)
            OnLoginSuccess?.Invoke(user);
        else
            ErrorMessage = "Невірний логін або пароль";
    }

    public event Action<User> OnLoginSuccess;
    public event Action OnShowRegister;
}