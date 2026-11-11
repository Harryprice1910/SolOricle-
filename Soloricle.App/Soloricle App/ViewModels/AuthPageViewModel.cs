using System.Windows.Input;

namespace SolOricle.App.ViewModels;

public class AuthPageViewModel
{
    public string Email { get; set; }
    public string Password { get; set; }

    public ICommand ContinueCommand { get; }

    public AuthPageViewModel()
    {
        ContinueCommand = new Command(async () =>
            await Shell.Current.GoToAsync("//ethics"));
    }
}