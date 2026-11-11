using SolOricle.App.ViewModels;

namespace SolOricle.App.views;

public partial class LandingPage : ContentPage
{

    public LandingPage() 
        {
            InitializeComponent();
            BindingContext = new LandingPageViewModel();
        }

        // The error CS0103: The name 'InitializeComponent' does not exist in the current context
        // occurs because the partial class 'LandingPage' expects a generated method 'InitializeComponent',
        // which is created by the XAML build process. This method is generated only if:
        // 1. The .xaml file exists with the same name (LandingPage.xaml) in the same namespace.
        // 2. The .xaml file's build action is set to 'MauiXaml' (for .NET MAUI) or 'Page' (for Xamarin.Forms/WPF).
        // 3. The project is built successfully so the code-behind is generated.
        //
        // To fix this:
        // - Ensure 'LandingPage.xaml' exists in the same folder and namespace.
        // - Check the .xaml file's build action in the properties window.
        // - Clean and rebuild the project to trigger code generation.
 }
     