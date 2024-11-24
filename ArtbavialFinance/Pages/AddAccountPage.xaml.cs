using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using ArtBavialMyFinance.Data.Models;
using ArtBavialMyFinance.Models;
using ArtBavialMyFinance.Data;
using ArtbavialFinance.Models;

namespace ArtbavialFinance.Pages
{
	public partial class AddAccountPage : ContentPage
	{
		private readonly AppDbContext _dbContext;
		private User currentUser;

		public AddAccountPage(AppDbContext dbContext, User user)
		{
			InitializeComponent();
			_dbContext = dbContext;
			currentUser = user; // Установка текущего пользователя
			LoadAccountTypes();
		}

		private async Task LoadCurrencies()
		{
			if (_dbContext != null)
			{
				var currencies = await _dbContext.Currencies
												 .Where(c => c.UserId == currentUser.Id) // Фильтруем валюты по текущему пользователю
												 .Include(c => c.User) // Используем Include для загрузки связанных данных
												 .ToListAsync();
				CurrencyPicker.ItemsSource = currencies;
				CurrencyPicker.ItemDisplayBinding = new Binding("Name");
			}
			else
			{
				await DisplayAlert("Ошибка", "Контекст базы данных не инициализирован.", "OK");
			}
		}

		protected override async void OnAppearing()
		{
			base.OnAppearing();
			await LoadCurrencies();
		}

		private void LoadAccountTypes()
		{
			AccountTypePicker.ItemsSource = Enum.GetValues(typeof(AccountType)).Cast<AccountType>().ToList();
		}

		private async void OnAddAccountClicked(object sender, EventArgs e)
		{
			try
			{
				var accountName = AccountNameEntry.Text;
				var balance = decimal.Parse(BalanceEntry.Text);
				var selectedCurrency = (Currency)CurrencyPicker.SelectedItem;
				var selectedAccountType = (AccountType)AccountTypePicker.SelectedItem;

				var isPrimaryAccount = PrimaryAccountCheckbox.IsChecked;

				// Проверка наличия главного счета
				if (isPrimaryAccount)
				{
					var existingPrimaryAccount = await _dbContext.Accounts
						.Where(a => a.IsPrimaryAccount && a.UserId == currentUser.Id) // Убедимся, что проверяем счета текущего пользователя
						.FirstOrDefaultAsync();

					if (existingPrimaryAccount != null)
					{
						bool answer = await DisplayAlert("Warning",
														 $"Главный счет уже существует: {existingPrimaryAccount.Name}. Вы хотите заменить его на новый главный счет?",
														 "Да",
														 "Нет");

						if (!answer)
						{
							return;
						}

						// Обновление текущего главного счета, чтобы он перестал быть главным
						existingPrimaryAccount.IsPrimaryAccount = false;
						_dbContext.Accounts.Update(existingPrimaryAccount);
					}
				}

				var account = new Account
				{
					Name = accountName,
					Balance = balance,
					CurrencyId = selectedCurrency.Id,
					Type = selectedAccountType,
					IsPrimaryAccount = isPrimaryAccount,
					UserId = currentUser.Id // Установите UserId для нового счета
				};

				_dbContext.Accounts.Add(account);
				await _dbContext.SaveChangesAsync();

				await DisplayAlert("Success", "Account added successfully!", "OK");

				// Возврат на предыдущую страницу
				await Navigation.PopAsync();
			}
			catch (Exception ex)
			{
				await DisplayAlert("Error", ex.Message, "OK");
			}
		}

		private async void OnAddCurrencyClicked(object sender, EventArgs e)
		{
			// Переход на страницу добавления валюты
			await Navigation.PushAsync(new AddCurrencyPage(_dbContext, currentUser));
		}
	}
}
