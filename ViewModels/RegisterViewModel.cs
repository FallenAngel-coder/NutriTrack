using System.Windows.Input;
using NutriTrack.Helpers;
using NutriTrack.Models;
using NutriTrack.Services;

namespace NutriTrack.ViewModels;

public class RegisterViewModel : BaseViewModel
{
    private readonly IAuthService _authService;

    private string _username;
    private string _password;
    private string _name;
    private int _age;
    private double _height;
    private double _weight;
    private Gender _selectedGender;
    private ActivityLevel _selectedActivity;
    private string _errorMessage;

    public string Username { get => _username; set => SetProperty(ref _username, value); }
    public string Password { get => _password; set => SetProperty(ref _password, value); }
    public string Name { get => _name; set => SetProperty(ref _name, value); }
    public int Age { get => _age; set => SetProperty(ref _age, value); }
    public double Height { get => _height; set => SetProperty(ref _height, value); }
    public double Weight { get => _weight; set => SetProperty(ref _weight, value); }
    public Gender SelectedGender { get => _selectedGender; set => SetProperty(ref _selectedGender, value); }
    public ActivityLevel SelectedActivity { get => _selectedActivity; set => SetProperty(ref _selectedActivity, value); }
    public string ErrorMessage { get => _errorMessage; set => SetProperty(ref _errorMessage, value); }

    public IEnumerable<Gender> Genders => Enum.GetValues(typeof(Gender)).Cast<Gender>();
    public IEnumerable<ActivityLevel> ActivityLevels => Enum.GetValues(typeof(ActivityLevel)).Cast<ActivityLevel>();

    public ICommand RegisterCommand { get; }
    public ICommand BackToLoginCommand { get; }

    public event Action<User> OnLoginSuccess;
    public event Action OnBackToLogin;

    public RegisterViewModel(IAuthService authService)
    {
        _authService = authService;

        SelectedGender = Gender.Male;
        SelectedActivity = ActivityLevel.Sedentary;

        RegisterCommand = new RelayCommand(async _ => await Register());
        BackToLoginCommand = new RelayCommand(_ => OnBackToLogin?.Invoke());
    }

    private async Task Register()
    {
        if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password) || string.IsNullOrWhiteSpace(Name))
        {
            ErrorMessage = "Заповніть основні поля (Логін, Пароль, Ім'я)!";
            return;
        }

        if (Weight <= 0 || Height <= 0 || Age <= 0)
        {
            ErrorMessage = "Введіть коректні антропометричні дані!";
            return;
        }

        var newUser = new User
        {
            Username = Username,
            Name = Name,
            Weight = Weight,
            Height = Height,
            Age = Age,
            UserGender = SelectedGender,
            Activity = SelectedActivity
        };

        try
        {
            var created = await _authService.RegisterAsync(newUser, Password);
            OnLoginSuccess?.Invoke(created);
        }
        catch (InvalidOperationException ex)
        {
            ErrorMessage = ex.Message;
        }
    }
}