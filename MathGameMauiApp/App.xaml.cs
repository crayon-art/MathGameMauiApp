using MathGameMauiApp.Data; 

namespace MathGameMauiApp;

public partial class App : Application
{
    public static GameRepo GameRepo { get; private set; }
    public App(GameRepo gameRepo)
    {
        InitializeComponent();

        MainPage = new AppShell();

        GameRepo = gameRepo;
    }
}
