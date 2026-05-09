using System.Configuration;
using System.Data;
using System.Globalization;
using System.Windows;
using System.Windows.Markup;
using NutriTrack.Data;
using NutriTrack.Data.Repositories;
using NutriTrack.Services;
using NutriTrack.ViewModels;
using NutriTrack.Views;

namespace NutriTrack;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private readonly IDispatcherService _dispatcher = new WpfDispatcherService();
    private readonly CalorieCalculator _calculator = new CalorieCalculator();

    protected override void OnStartup(StartupEventArgs e)
    {
        FrameworkElement.LanguageProperty.OverrideMetadata(
            typeof(FrameworkElement),
            new FrameworkPropertyMetadata(
                XmlLanguage.GetLanguage(CultureInfo.InvariantCulture.IetfLanguageTag)));

        base.OnStartup(e);

        var context = new AppDbContext();
        context.Database.EnsureCreated();
        var repo = new SqliteNutriRepository(context);
        var authService = new AuthService(repo);
        var foodLogService = new FoodLogService(repo);

        ShowLogin(authService, repo, foodLogService);
    }

    private void ShowLogin(IAuthService authService, SqliteNutriRepository repo, IFoodLogService foodLogService)
    {
        var loginWindow = new LoginWindow();
        var loginVM = new LoginViewModel(authService);
        loginWindow.DataContext = loginVM;

        loginVM.OnLoginSuccess += (user) => {
            var mainWin = new MainWindow(user, repo, _dispatcher, _calculator, foodLogService);
            mainWin.Show();
            loginWindow.Close();
        };

        loginVM.OnShowRegister += () => {
            ShowRegister(authService, repo, foodLogService);
            loginWindow.Close();
        };

        loginWindow.Show();
    }

    private void ShowRegister(IAuthService authService, SqliteNutriRepository repo, IFoodLogService foodLogService)
    {
        var regWindow = new RegisterWindow();
        var regVM = new RegisterViewModel(authService);
        regWindow.DataContext = regVM;

        regVM.OnLoginSuccess += (user) => {
            var mainWin = new MainWindow(user, repo, _dispatcher, _calculator, foodLogService);
            mainWin.Show();
            regWindow.Close();
        };

        regVM.OnBackToLogin += () => {
            ShowLogin(authService, repo, foodLogService);
            regWindow.Close();
        };

        regWindow.Show();
    }
}