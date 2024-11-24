using ArtbavialMyFinance.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using ArtbavialMyFinance.Models;
using ArtBavialMyFinance.Data.Models;
using ArtbavialFinance.Pages;

namespace ArtBavialFinance
{
	public partial class MainPage : ContentPage
	{
		private readonly AppDbContext _dbContext;

		public MainPage(AppDbContext dbContext)
		{
			InitializeComponent();
			_dbContext = dbContext;
			LoadDashboardData();
		}

		private async void LoadDashboardData()
		{
			await LoadTotalBalance();
			await LoadRecentTransactions();
		}

		private async Task LoadTotalBalance()
		{
			var totalBalance = await _dbContext.Accounts.SumAsync(a => a.Balance);
			TotalBalanceLabel.Text = $"{totalBalance:F2} USD"; // Здесь вы можете изменить валюту на нужную
		}

		private async Task LoadRecentTransactions()
		{
			var transactions = await _dbContext.Transactions
												.OrderByDescending(t => t.Date)
												.Take(10)
												.ToListAsync();
			TransactionsListView.ItemsSource = transactions;
		}

		private async void OnAddAccountClicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new AddAccountPage(_dbContext));
		}

		private async void OnAddTransactionClicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new AddTransactionPage(_dbContext));
		}
	}
}
