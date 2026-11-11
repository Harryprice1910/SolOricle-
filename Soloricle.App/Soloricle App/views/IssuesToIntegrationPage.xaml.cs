using SolOricle.App.ViewModels;

namespace SolOricle.App.views;

public partial class IssuesToIntegrationPage : ContentPage
{
    public IssuesToIntegrationPage()
    {
        InitializeComponent();

        BindingContext = new IssuesToIntegrationPageViewModel();
    }
}