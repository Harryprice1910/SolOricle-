using System.Windows.Input;

namespace SolOricle.App.ViewModels;

public class IssuesToIntegrationPageViewModel
{
    public string IntegrationText { get; set; } = string.Empty;

    public ICommand FinishCommand { get; }

    public IssuesToIntegrationPageViewModel()
    {
        FinishCommand = new Command(async () =>
        {
            await Shell.Current.GoToAsync("//landing");
        });
    }
}