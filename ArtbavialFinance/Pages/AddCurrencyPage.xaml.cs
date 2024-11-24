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
	public partial class AddCurrencyPage : ContentPage
	{
		private readonly AppDbContext _dbContext;
		private User currentUser;

		public AddCurrencyPage(AppDbContext dbContext, User user)
		{
			_dbContext = dbContext;
			currentUser = user; // Установка текущего пользователя
			InitializeComponent();
		}


		private async void OnAddCurrencyClicked(object sender, EventArgs e)
		{
			var currencyName = currencyNameEntry.Text;
			var currencySymbol = symbolEntry.Text;
			var exchangeRate = decimal.Parse(exchangeRateEntry.Text);
			var codeCurrency = codeCurrencyEntry.Text;
			var isBaseCurrency = BaseCurrencyCheckbox.IsChecked;

			// Проверка наличия валюты в базе данных
			var existingCurrency = await _dbContext.Currencies
												   .Where(c => c.UserId == currentUser.Id && (c.CodeCurrency == codeCurrency || c.Symbol == currencySymbol))
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
														   .Where(c => c.UserId == currentUser.Id && c.IsBaseCurrency)
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
				UserId = currentUser.Id // Установите UserId для новой валюты
			};

			_dbContext.Currencies.Add(currency);
			await _dbContext.SaveChangesAsync();

			await DisplayAlert("Success", "Currency added successfully!", "OK");
			// Возврат на главную страницу
			await Navigation.PopAsync();
		}
	}
}
