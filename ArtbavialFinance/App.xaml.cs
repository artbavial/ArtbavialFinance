using Microsoft.Maui.Controls;
using Microsoft.Extensions.DependencyInjection;
using ArtBavialMyFinance.Data;

namespace ArtbavialFinance
{
	public partial class App : Application
	{
		public App(IServiceProvider serviceProvider)
		{
			InitializeComponent();
			MainPage = new NavigationPage(serviceProvider.GetRequiredService<Pages.LoginPage>()); // Получаем LoginPage из контейнера зависимостей
		}
	}
}