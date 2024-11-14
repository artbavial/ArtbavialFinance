using ArtbavialFinance.Pages;
using ArtbavialFinance;
using ArtbavialMyFinance.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

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
			var username = UsernameEntry.Text;
			var password = PasswordEntry.Text;

			var user = await _dbContext.Users.SingleOrDefaultAsync(u => u.Username == username);
			if (user != null && BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
			{
				// Успешная авторизация, перенаправляем на MainPage
				await Navigation.PushAsync(new MainPage(user)); // Передаем текущего пользователя в MainPage
			}
			else
			{
				MessageLabel.Text = "Invalid username or password.";
			}
		}

		private async void OnRegisterButtonClicked(object sender, EventArgs e)
		{
			// Переход на страницу регистрации
			await Navigation.PushAsync(new RegisterPage(_dbContext));
		}
	}
}
