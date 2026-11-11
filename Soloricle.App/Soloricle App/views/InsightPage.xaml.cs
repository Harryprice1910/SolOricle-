using SolOricle.App.ViewModels;

namespace SolOricle.App.views;

public partial class InsightPage : ContentPage
{
    public InsightPage()
    {
        InitializeComponent();
        BindingContext = new InsightPageViewModel();
    }

    private async void NextButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//integration");
    }
}