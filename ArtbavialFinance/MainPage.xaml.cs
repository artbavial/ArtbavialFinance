using Microsoft.Maui.Controls;
using ArtbavialMyFinance.Models;

namespace ArtbavialFinance
{
	public partial class MainPage : ContentPage
	{
		private readonly User _currentUser;

		public MainPage(User currentUser)
		{
			InitializeComponent();
			_currentUser = currentUser;

			// Отображение информации о пользователе
			UserInfoLabel.Text = $"Logged in as: {_currentUser.Username}";
		}

		private async void OnLogoutClicked(object sender, EventArgs e)
		{
			// Возвращаемся на страницу входа
			await Navigation.PopToRootAsync(); // Возвращаемся на корневую страницу (LoginPage)
		}
	}
}