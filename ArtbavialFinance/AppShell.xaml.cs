using ArtbavialFinance.Pages;
using ArtBavialFinance;

namespace ArtbavialFinance
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

			// Регистрация маршрутов для навигации
			Routing.RegisterRoute("main", typeof(MainPage));
			Routing.RegisterRoute("currencies", typeof(ManageCurrenciesPage));
		}
    }
}