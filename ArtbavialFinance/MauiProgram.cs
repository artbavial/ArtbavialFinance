using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ArtbavialMyFinance.Models;
using ArtBavialMyFinance.Data.Models;
using ArtbavialFinance.Pages;
using ArtbavialMyFinance.Data;
using Microsoft.Extensions.Configuration;
using ArtBavialFinance;

namespace ArtbavialFinance
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
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
;
			// Регистрация DbContext без строки подключения
			builder.Services.AddDbContext<AppDbContext>();

			// Регистрация страниц
			builder.Services.AddTransient<Pages.RegisterPage>();
			builder.Services.AddTransient<Pages.LoginPage>();
			builder.Services.AddTransient<MainPage>(); // Регистрация MainPage


#if DEBUG
			builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
