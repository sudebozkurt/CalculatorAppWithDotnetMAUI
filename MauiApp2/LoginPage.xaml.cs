namespace MauiApp2;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
	}

    private void LoginClicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new CalculatorPage());
    }

    private void RegisterClicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new RegisterPage());
    }
}