using System.Windows.Input;

namespace SolOricle.App.ViewModels;

public class EthicsPageViewModel
{
    public ICommand AcceptCommand { get; }

    public EthicsPageViewModel()
    {
        AcceptCommand = new Command(async () =>
            await Shell.Current.GoToAsync("//example"));
    }
}