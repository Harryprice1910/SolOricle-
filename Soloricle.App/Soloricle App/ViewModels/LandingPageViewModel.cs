using System.Windows.Input;

namespace SolOricle.App.ViewModels;

public class LandingPageViewModel
{
    public ICommand BeginCommand { get; }

    public LandingPageViewModel()
    {
        BeginCommand = new Command(async () =>
            await Shell.Current.GoToAsync("//auth"));
    }
}