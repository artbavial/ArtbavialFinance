using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using ArtBavialMyFinance.Data.Models;
using ArtBavialMyFinance.Models;
using ArtBavialMyFinance.Data;
using ArtbavialFinance.Models;
using System.Collections.ObjectModel;

namespace ArtbavialFinance.Pages
{
	public partial class AddCurrencyPage : ContentPage
	{
		private readonly AppDbContext _dbContext;
		private User _currentUser;
		public ObservableCollection<Currency> Currencies { get; set; }

		public AddCurrencyPage(AppDbContext dbContext, User user)
		{
			_dbContext = dbContext;
			_currentUser = user;
			InitializeComponent();
			Currencies = new ObservableCollection<Currency>();
			CurrencyCollectionView.ItemsSource = Currencies;
			LoadCurrencies();
		}

		private async Task LoadCurrencies()
		{
			var currencies = await _dbContext.Currencies
											 .Where(c => c.UserId == _currentUser.Id)
											 .ToListAsync();

			foreach (var currency in currencies)
			{
				Currencies.Add(currency);
			}
		}

		private async void OnAddCurrencyClicked(object sender, EventArgs e)
		{
			string currencyName = await DisplayPromptAsync("Add Currency", "Enter currency name:");
			string currencySymbol = await DisplayPromptAsync("Add Currency", "Enter currency symbol:");
			string exchangeRateString = await DisplayPromptAsync("Add Currency", "Enter exchange rate:");
			string codeCurrency = await DisplayPromptAsync("Add Currency", "Enter code currency:");
			bool isBaseCurrency = await DisplayAlert("Add Currency", "Is this the base currency?", "Yes", "No");

			if (decimal.TryParse(exchangeRateString, out decimal exchangeRate))
			{
				// Проверка наличия валюты в базе данных
				var existingCurrency = await _dbContext.Currencies
													   .Where(c => c.UserId == _currentUser.Id && (c.CodeCurrency == codeCurrency || c.Symbol == currencySymbol))
													   .FirstOrDefaultAsync();

				if (existingCurrency != null)
				{
					await DisplayAlert("Error", $"Такая валюта уже существует! Ее название: {existingCurrency.Name}", "OK");
					return;
				}

				// Проверка наличия базовой валюты
				if (isBaseCurrency)
				{
					var existingBaseCurrency = await _dbContext.Currencies
															   .Where(c => c.UserId == _currentUser.Id && c.IsBaseCurrency)
															   .FirstOrDefaultAsync();

					if (existingBaseCurrency != null)
					{
						bool answer = await DisplayAlert("Warning",
														 $"Базовая валюта уже существует: {existingBaseCurrency.Name}. Вы хотите заменить её на новую базовую валюту?",
														 "Да",
														 "Нет");

						if (!answer)
						{
							return;
						}

						// Обновление текущей базовой валюты, чтобы она перестала быть базовой
						existingBaseCurrency.IsBaseCurrency = false;
						_dbContext.Currencies.Update(existingBaseCurrency);
					}
				}

				var currency = new Currency
				{
					Name = currencyName,
					Symbol = currencySymbol,
					ExchangeRate = exchangeRate,
					CodeCurrency = codeCurrency,
					IsBaseCurrency = isBaseCurrency,
					UserId = _currentUser.Id
				};

				_dbContext.Currencies.Add(currency);
				await _dbContext.SaveChangesAsync();

				Currencies.Add(currency);

				await DisplayAlert("Success", "Currency added successfully!", "OK");
			}
			else
			{
				await DisplayAlert("Error", "Invalid exchange rate", "OK");
			}
		}

		private async void OnCurrencySelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			var selectedCurrency = e.CurrentSelection.FirstOrDefault() as Currency;
			if (selectedCurrency != null)
			{
				string newName = await DisplayPromptAsync("Edit Currency", "Enter new currency name:", initialValue: selectedCurrency.Name);
				string newSymbol = await DisplayPromptAsync("Edit Currency", "Enter new currency symbol:", initialValue: selectedCurrency.Symbol);
				string newExchangeRateString = await DisplayPromptAsync("Edit Currency", "Enter new exchange rate:", initialValue: selectedCurrency.ExchangeRate.ToString());
				string newCodeCurrency = await DisplayPromptAsync("Edit Currency", "Enter new code currency:", initialValue: selectedCurrency.CodeCurrency);
				bool newIsBaseCurrency = await DisplayAlert("Edit Currency", "Is this the base currency?", "Yes", "No");

				if (decimal.TryParse(newExchangeRateString, out decimal newExchangeRate))
				{
					selectedCurrency.Name = newName;
					selectedCurrency.Symbol = newSymbol;
					selectedCurrency.ExchangeRate = newExchangeRate;
					selectedCurrency.CodeCurrency = newCodeCurrency;
					selectedCurrency.IsBaseCurrency = newIsBaseCurrency;

					if (selectedCurrency.IsBaseCurrency)
					{
						var existingBaseCurrency = await _dbContext.Currencies
																   .Where(c => c.UserId == selectedCurrency.UserId && c.IsBaseCurrency && c.Id != selectedCurrency.Id)
																   .FirstOrDefaultAsync();

						if (existingBaseCurrency != null)
						{
							existingBaseCurrency.IsBaseCurrency = false;
							_dbContext.Currencies.Update(existingBaseCurrency);
						}
					}

					_dbContext.Currencies.Update(selectedCurrency);
					await _dbContext.SaveChangesAsync();

					// Обновление отображаемого списка
					Currencies.Clear();
					await LoadCurrencies();

					await DisplayAlert("Success", "Currency updated successfully!", "OK");
				}
				else
				{
					await DisplayAlert("Error", "Invalid exchange rate", "OK");
				}
			}
		}
	}
}
