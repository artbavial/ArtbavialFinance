using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ArtbavialFinance.Data;
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
			var username = UsernameEntry.Text;
			var password = PasswordEntry.Text;

			var user = await _dbContext.Users.SingleOrDefaultAsync(u => u.Username == username);
			if (user != null && BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
			{
				// Успешная авторизация, перенаправляем на MainPage
				await Navigation.PushAsync(new MainPage(user));
			}
			else
			{
				MessageLabel.Text = "Invalid username or password.";
			}
		}
	}
}