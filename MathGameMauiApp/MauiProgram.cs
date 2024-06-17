using Microsoft.Extensions.Logging;
using MathGameMauiApp.Data;

namespace MathGameMauiApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("CaveatBrush-Regular.ttf", "CaveatBrush-Regular");
                });

            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "game.db");


            builder.Services.AddSingleton(s =>
                ActivatorUtilities.CreateInstance<GameRepo>(s, dbPath));    

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
