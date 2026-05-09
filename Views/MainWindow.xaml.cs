using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using NutriTrack.Data;
using NutriTrack.Data.Repositories;
using NutriTrack.Models;
using NutriTrack.Services;
using NutriTrack.ViewModels;

namespace NutriTrack.Views;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow(User user, INutriRepository repository, IDispatcherService dispatcherService, CalorieCalculator calculator, IFoodLogService foodLogService)
    {
        InitializeComponent();
        this.DataContext = new MainViewModel(repository, user, dispatcherService, calculator, foodLogService);
    }
}