using SolOricle.App.ViewModels;

namespace SolOricle.App.views;

public partial class EthicsPage : ContentPage
{
    public EthicsPage()
    {
        InitializeComponent();
        BindingContext = new EthicsPageViewModel();
    }
}