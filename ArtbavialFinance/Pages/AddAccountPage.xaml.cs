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
	public partial class AddAccountPage : ContentPage
	{
		private readonly AppDbContext _dbContext;

		public AddAccountPage(AppDbContext dbContext)
		{
			InitializeComponent();
			_dbContext = dbContext;
			LoadAccountTypes();
		}

		private async Task LoadCurrencies()
		{
			if (_dbContext != null)
			{
				var currencies = await _dbContext.Currencies.ToListAsync();
				CurrencyPicker.ItemsSource = currencies;
				CurrencyPicker.ItemDisplayBinding = new Binding("Name");
			}
			else
			{
				await DisplayAlert("������", "�������� ���� ������ �� ���������������.", "OK");
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

				// �������� ������� �������� �����
				if (isPrimaryAccount)
				{
					var existingPrimaryAccount = await _dbContext.Accounts
						.Where(a => a.IsPrimaryAccount)
						.FirstOrDefaultAsync();

					if (existingPrimaryAccount != null)
					{
						bool answer = await DisplayAlert("Warning",
														 $"������� ���� ��� ����������: {existingPrimaryAccount.Name}. �� ������ �������� ��� �� ����� ������� ����?",
														 "��",
														 "���");

						if (!answer)
						{
							return;
						}

						// ���������� �������� �������� �����, ����� �� �������� ���� �������
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
					IsPrimaryAccount = isPrimaryAccount
				};

				_dbContext.Accounts.Add(account);
				await _dbContext.SaveChangesAsync();

				await DisplayAlert("Success", "Account added successfully!", "OK");

				// ������� �� ���������� ��������
				await Navigation.PopAsync();
			}
			catch (Exception ex)
			{
				await DisplayAlert("Error", ex.Message, "OK");
			}
		}

		private async void OnAddCurrencyClicked(object sender, EventArgs e)
		{
			// ������� �� �������� ���������� ������
			await Navigation.PushAsync(new AddCurrencyPage(_dbContext));
		}
	}
}
