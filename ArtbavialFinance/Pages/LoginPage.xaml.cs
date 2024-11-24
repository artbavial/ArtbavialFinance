
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using ArtBavialFinance;
using ArtBavialMyFinance.Data;
using ArtbavialFinance.Models;

namespace ArtbavialFinance.Pages
{
	public partial class LoginPage : ContentPage
	{
		private readonly AppDbContext _dbContext;

		public LoginPage(AppDbContext dbContext)
		{
			InitializeComponent();
			_dbContext = dbContext;
		}

		private async void OnLoginClicked(object sender, EventArgs e)
		{
			try
			{
				var username = UsernameEntry.Text;
				var password = PasswordEntry.Text;

				var user = await _dbContext.Users.SingleOrDefaultAsync(u => u.Username == username);
				if (user != null && BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
				{
					// ”спешна€ авторизаци€, устанавливаем MainPage как корневую страницу
					Application.Current.MainPage = new NavigationPage(new MainPage(_dbContext, user));
				}
				else
				{
					MessageLabel.Text = "Invalid username or password.";
				}
			}
			catch (Exception ex)
			{
				await DisplayAlert("Error", ex.Message, "OK");
			}
		}

		private async void OnRegisterButtonClicked(object sender, EventArgs e)
		{
			try
			{
				// ѕереход на страницу регистрации
				await Navigation.PushAsync(new RegisterPage(_dbContext));
			}
			catch (Exception ex)
			{
				await DisplayAlert("Error", ex.Message, "OK");
			}
		}
	}
}
