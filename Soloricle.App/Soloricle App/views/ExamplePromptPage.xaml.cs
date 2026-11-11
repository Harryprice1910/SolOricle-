using SolOricle.App.ViewModels;


namespace SolOricle.App.views;

public partial class ExamplePromptPage : ContentPage
{
    public ExamplePromptPage()
    {
        InitializeComponent();
        BindingContext = new ExamplePromptPageViewModel();
    }
}