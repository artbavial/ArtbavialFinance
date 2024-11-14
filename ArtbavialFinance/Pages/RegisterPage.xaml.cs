using ArtbavialFinance.Data;
using ArtbavialFinance.Models;
using Microsoft.EntityFrameworkCore;

namespace ArtbavialFinance.Pages;

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
		var email = EmailEntry.Text;
		var password = PasswordEntry.Text;

		// ѕроверка на существование пользовател€
		if (await _dbContext.Users.AnyAsync(u => u.Username == username || u.Email == email))
		{
			MessageLabel.Text = "Username or email already exists.";
			return;
		}

		var user = new User
		{
			Username = username,
			Email = email,
			PasswordHash = BCrypt.Net.BCrypt.HashPassword(password)
		};

		_dbContext.Users.Add(user);
		await _dbContext.SaveChangesAsync();
		MessageLabel.Text = "User  registered successfully!";
	}
}