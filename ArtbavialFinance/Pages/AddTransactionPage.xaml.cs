using System;
using ArtbavialFinance.Models;
using ArtBavialMyFinance.Data;
using ArtBavialMyFinance.Data.Models;
using ArtBavialMyFinance.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Maui.Controls;

namespace ArtbavialFinance.Pages
{
	public partial class AddTransactionPage : ContentPage
	{
		private readonly AppDbContext _dbContext;
		private User currentUser;

		public AddTransactionPage(AppDbContext dbContext, User user)
		{
			InitializeComponent();
			_dbContext = dbContext;
			currentUser = user; // Установка текущего пользователя
			
		}

		protected override async void OnAppearing()
		{
			base.OnAppearing();
			await LoadAccounts();
			LoadTransactionTypes();
		}

		private async Task LoadAccounts()
		{
			if (_dbContext != null)
			{
				var accounts = await _dbContext.Accounts
											   .Where(a => a.UserId == currentUser.Id) // Фильтруем счета по текущему пользователю
											   .Include(a => a.User) // Используем Include для загрузки связанных данных
											   .ToListAsync();
				AccountPicker.ItemsSource = accounts;
				AccountPicker.ItemDisplayBinding = new Binding("Name");
			}
			else
			{
				await DisplayAlert("Ошибка", "Контекст базы данных не инициализирован.", "OK");
			}
		}

		private void LoadTransactionTypes()
		{
			// Заполнение Picker данными из enum TransactionType
			TransactionTypePicker.ItemsSource = Enum.GetValues(typeof(TransactionType)).Cast<TransactionType>().ToList();
		}

		private async void OnSaveTransactionClicked(object sender, EventArgs e)
		{
			try
			{
				var amount = decimal.Parse(AmountEntry.Text);
				var selectedAccount = (Account)AccountPicker.SelectedItem;
				var transactionType = (TransactionType)TransactionTypePicker.SelectedItem;
				var description = DescriptionEditor.Text;

				var transaction = new Transaction
				{
					Amount = amount,
					Date = DateTime.Now,
					Type = transactionType,
					Description = description,
					AccountId = selectedAccount.Id,
					UserId = currentUser.Id // Установите UserId для новой транзакции
				};

				_dbContext.Transactions.Add(transaction);
				await _dbContext.SaveChangesAsync();

				await DisplayAlert("Success", "Transaction added successfully!", "OK");

				// Возврат на предыдущую страницу
				await Navigation.PopAsync();
			}
			catch (Exception ex)
			{
				await DisplayAlert("Error", ex.Message, "OK");
			}
		}
	}
}
