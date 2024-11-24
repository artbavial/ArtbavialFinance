using System;
using System.Linq;
using Microsoft.Maui.Controls;
using ArtBavialMyFinance.Models;
using ArtBavialMyFinance.Services;

namespace ArtbavialFinance.Pages
{
	public partial class ManageCurrenciesPage : ContentPage
	{
		private readonly FinanceManager _financeManager;
		private readonly long _currentUserId;
		private Currency _selectedCurrency;

		public ManageCurrenciesPage(FinanceManager financeManager, long currentUserId)
		{
			InitializeComponent();
			_financeManager = financeManager;
			_currentUserId = currentUserId;

			LoadCurrencies();
			LoadBaseCurrencies();
		}

		private void LoadCurrencies()
		{
			CurrencyListView.ItemsSource = _financeManager.GetUserCurrencies(_currentUserId);
		}

		private void LoadBaseCurrencies()
		{
			BaseCurrencyPicker.ItemsSource = _financeManager.GetUserCurrencies(_currentUserId).Where(c => c.IsBaseCurrency).ToList();
		}

		private void OnAddCurrencyClicked(object sender, EventArgs e)
		{
			string codeCurrency = CurrencyCodeCurrencyEntry.Text;
			string name = CurrencyNameEntry.Text;
			string symbol = CurrencySymbolEntry.Text;
			var baseCurrency = (Currency)BaseCurrencyPicker.SelectedItem;
			decimal exchangeRate;

			if (string.IsNullOrWhiteSpace(codeCurrency) || string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(symbol) || baseCurrency == null || !decimal.TryParse(ExchangeRateEntry.Text, out exchangeRate))
			{
				DisplayAlert("Error", "Please enter valid values for all fields.", "OK");
				return;
			}

			var newCurrency = new Currency
			{
				CodeCurrency = codeCurrency,
				Name = name,
				Symbol = symbol,
				ExchangeRate = exchangeRate,
				IsBaseCurrency = false,
				UserId = _currentUserId
			};

			_financeManager.AddCurrencyToUser(_currentUserId, newCurrency);

			LoadCurrencies();

			CurrencyCodeCurrencyEntry.Text = string.Empty;
			CurrencyNameEntry.Text = string.Empty;
			CurrencySymbolEntry.Text = string.Empty;
			ExchangeRateEntry.Text = string.Empty;

			DisplayAlert("Success", "Currency added successfully.", "OK");
		}

		private void OnCurrencySelected(object sender, SelectedItemChangedEventArgs e)
		{
			_selectedCurrency = (Currency)e.SelectedItem;

			if (_selectedCurrency != null)
			{
				CurrencyCodeCurrencyEntry.Text = _selectedCurrency.CodeCurrency;
				CurrencyNameEntry.Text = _selectedCurrency.Name;
				CurrencySymbolEntry.Text = _selectedCurrency.Symbol;
				ExchangeRateEntry.Text = _selectedCurrency.ExchangeRate.ToString();
				BaseCurrencyPicker.SelectedItem = _financeManager.GetUserCurrencies(_currentUserId).FirstOrDefault(c => c.Id == _selectedCurrency.UserId && c.IsBaseCurrency);

				SaveChangesButton.IsVisible = true;
			}
		}

		private void OnSaveCurrencyChangesClicked(object sender, EventArgs e)
		{
			if (_selectedCurrency == null)
				return;

			string codeCurrency = CurrencyCodeCurrencyEntry.Text;
			string name = CurrencyNameEntry.Text;
			string symbol = CurrencySymbolEntry.Text;
			var baseCurrency = (Currency)BaseCurrencyPicker.SelectedItem;
			decimal exchangeRate;

			if (string.IsNullOrWhiteSpace(codeCurrency) || string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(symbol) || baseCurrency == null || !decimal.TryParse(ExchangeRateEntry.Text, out exchangeRate))
			{
				DisplayAlert("Error", "Please enter valid values for all fields.", "OK");
				return;
			}

			_selectedCurrency.CodeCurrency = codeCurrency;
			_selectedCurrency.Name = name;
			_selectedCurrency.Symbol = symbol;
			_selectedCurrency.ExchangeRate = exchangeRate;

			LoadCurrencies();

			CurrencyCodeCurrencyEntry.Text = string.Empty;
			CurrencyNameEntry.Text = string.Empty;
			CurrencySymbolEntry.Text = string.Empty;
			ExchangeRateEntry.Text = string.Empty;

			SaveChangesButton.IsVisible = false;

			DisplayAlert("Success", "Currency updated successfully.", "OK");
		}
	}
}
