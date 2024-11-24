using ArtbavialMyFinance.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using ArtbavialMyFinance.Models;
using ArtBavialMyFinance.Data.Models;

namespace ArtbavialFinance.Pages
{
	public partial class AddCurrencyPage : ContentPage
	{
		private readonly AppDbContext _dbContext;

		public AddCurrencyPage(AppDbContext dbContext)
		{
			_dbContext = dbContext;
			InitializeComponent();

			LoadCurrencies();
		}

		private async void LoadCurrencies()
		{
			var currencies = await _dbContext.Currencies.ToListAsync();
		}

		private async void OnAddAccountClicked(object sender, EventArgs e)
		{
			try
			{
				var currencyName = currencyNameEntry.Text;
				var currencySymbol = symbolEntry.Text;
				var exchangeRate = decimal.Parse(exchangeRateEntry.Text);
				var codeCurrency = codeCurrencyEntry.Text;
				var isBaseCurrency = BaseCurrencyCheckbox.IsChecked;

				// �������� ������� ������ � ���� ������
				var existingCurrency = await _dbContext.Currencies
					.Where(c => c.CodeCurrency == codeCurrency || c.Symbol == currencySymbol)
					.FirstOrDefaultAsync();

				if (existingCurrency != null)
				{
					await DisplayAlert("Error", "����� ������ ��� ����������!", "OK");
					return;
				}

				// �������� ������� ������� ������
				if (isBaseCurrency)
				{
					var existingBaseCurrency = await _dbContext.Currencies
						.Where(c => c.IsBaseCurrency)
						.FirstOrDefaultAsync();

					if (existingBaseCurrency != null)
					{
						bool answer = await DisplayAlert("Warning",
														 $"������� ������ ��� ����������: {existingBaseCurrency.Name}. �� ������ �������� � �� ����� ������� ������?",
														 "��",
														 "���");

						if (!answer)
						{
							return;
						}

						// ���������� ������� ������� ������, ����� ��� ��������� ���� �������
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
					IsBaseCurrency = isBaseCurrency
				};

				_dbContext.Currencies.Add(currency);
				await _dbContext.SaveChangesAsync();

				await DisplayAlert("Success", "Currency added successfully!", "OK");
				// ������� �� ������� ��������
				await Navigation.PopAsync();
			}
			catch (Exception ex)
			{
				await DisplayAlert("Error", ex.Message, "OK");
			}
		}
	}
}
