namespace MathGameMauiApp;

public partial class PreviousGames : ContentPage
{
	public PreviousGames() 
	{
		InitializeComponent();

		gamesList.ItemsSource = App.GameRepo.GetAllGAmes();
	}

	public void OnBackToMenu(object sender, EventArgs e)
	{
		Navigation.PushAsync(new MainPage());
	}
	private void OnDelete (object sender, EventArgs e)
	{
		ImageButton button = (ImageButton)sender;

		App.GameRepo.Delete((int)button.BindingContext);

        gamesList.ItemsSource = App.GameRepo.GetAllGAmes();
    }
}