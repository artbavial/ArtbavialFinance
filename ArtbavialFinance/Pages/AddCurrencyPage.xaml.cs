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
			currentUser = user; // ��������� �������� ������������
			InitializeComponent();
		}


		private async void OnAddCurrencyClicked(object sender, EventArgs e)
		{
			var currencyName = currencyNameEntry.Text;
			var currencySymbol = symbolEntry.Text;
			var exchangeRate = decimal.Parse(exchangeRateEntry.Text);
			var codeCurrency = codeCurrencyEntry.Text;
			var isBaseCurrency = BaseCurrencyCheckbox.IsChecked;

			// �������� ������� ������ � ���� ������
			var existingCurrency = await _dbContext.Currencies
												   .Where(c => c.UserId == currentUser.Id && (c.CodeCurrency == codeCurrency || c.Symbol == currencySymbol))
												   .FirstOrDefaultAsync();

			if (existingCurrency != null)
			{
				await DisplayAlert("Error", $"����� ������ ��� ����������! �� ��������: {existingCurrency.Name}", "OK");
				return;
			}

			// �������� ������� ������� ������
			if (isBaseCurrency)
			{
				var existingBaseCurrency = await _dbContext.Currencies
														   .Where(c => c.UserId == currentUser.Id && c.IsBaseCurrency)
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
				IsBaseCurrency = isBaseCurrency,
				UserId = currentUser.Id // ���������� UserId ��� ����� ������
			};

			_dbContext.Currencies.Add(currency);
			await _dbContext.SaveChangesAsync();

			await DisplayAlert("Success", "Currency added successfully!", "OK");
			// ������� �� ������� ��������
			await Navigation.PopAsync();
		}
	}
}
