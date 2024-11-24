
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using ArtbavialFinance.Models;
using ArtBavialMyFinance.Data;

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

				var username = UsernameEntry.Text;
				var password = PasswordEntry.Text;
				var email = EmailEntry.Text;

				var userExists = await _dbContext.Users.AnyAsync(u => u.Username == username);
				if (userExists)
				{
					MessageLabel.TextColor = Colors.Red;
					MessageLabel.Text = "Username already exists.";
					return;
				}

				var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
				var user = new User { Username = username, PasswordHash = hashedPassword, Email = email };

				_dbContext.Users.Add(user);
				await _dbContext.SaveChangesAsync();

				MessageLabel.TextColor = Colors.Green; // Изменяем цвет текста на зеленый
				MessageLabel.Text = "User registered successfully!";
			

		}
	}
}
