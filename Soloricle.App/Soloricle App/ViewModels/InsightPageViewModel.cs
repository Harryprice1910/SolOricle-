using System.Windows.Input;

namespace SolOricle.App.ViewModels;

public class InsightPageViewModel
{
    public string InsightText { get; } =
        "Reflection reveals a tension between intention and habit.";

    public ICommand IntegrateCommand { get; }

    public InsightPageViewModel()
    {
        IntegrateCommand = new Command(async () =>
            await Shell.Current.GoToAsync("//Integration"));
    }
}