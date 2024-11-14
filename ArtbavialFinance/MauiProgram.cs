using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ArtbavialFinance.Data;
using ArtbavialFinance.Pages;

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

			// Настройка подключения к базе данных
			var connectionString = "\"Server=mssql02.by2040.hb.by;packet size=4096;user id=artbavial;pwd=VITbar231089;data source=ArtbavialFinanceDB;persist security info=False;initial catalog=ArtbavialFinanceDB;";
			builder.Services.AddDbContext<AppDbContext>(options =>
				options.UseSqlServer(connectionString));

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
