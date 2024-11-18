using ArtbavialMyFinance.Data; // Восстановлено пространство имен
using ArtbavialMyFinance.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace ArtbavialFinance.Pages
{
	public partial class RegisterPage : ContentPage
	{
		private readonly AppDbContext _dbContext;

		public RegisterPage(AppDbContext dbContext)
		{
			InitializeComponent();
			_dbContext = dbContext;
		}

		private async void OnRegisterClicked(object sender, EventArgs e)
		{
			try
			{
				var username = UsernameEntry.Text;
				var password = PasswordEntry.Text;

				var userExists = await _dbContext.Users.AnyAsync(u => u.Username == username);
				if (userExists)
				{
					MessageLabel.TextColor = Colors.Red;
					MessageLabel.Text = "Username already exists.";
					return;
				}

				var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
				var user = new User { Username = username, PasswordHash = hashedPassword };

				_dbContext.Users.Add(user);
				await _dbContext.SaveChangesAsync();

				MessageLabel.TextColor = Colors.Green; // Изменяем цвет текста на зеленый
				MessageLabel.Text = "User registered successfully!";
			}
			catch (Exception ex)
			{
				await DisplayAlert("Error", ex.Message, "OK");
			}
		}
	}
}
