using SolOricle.App.ViewModels;

namespace SolOricle.App.views;

public partial class AuthPage : ContentPage
{
    public AuthPage()
    {
        InitializeComponent();
        BindingContext = new AuthPageViewModel();
    }
}