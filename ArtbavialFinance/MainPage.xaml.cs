using ArtbavialMyFinance.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using ArtBavialMyFinance.Data.Models;
using ArtbavialFinance.Pages;
using System.Windows.Input;
using ArtBavialMyFinance.Data;
using ArtbavialFinance.Models;

namespace ArtBavialFinance
{
	public partial class MainPage : ContentPage
	{
		private readonly AppDbContext _dbContext;
		public User CurrentUser { get; private set; }
		public ICommand NavigateCommand { get; private set; }
		public MainPage(AppDbContext dbContext, User user)
		{
			InitializeComponent();
			NavigateCommand = new Command<string>(OnNavigate);
			BindingContext = this;
			_dbContext = dbContext;
			CurrentUser = user;
			LoadDashboardData();
		}

		private async void OnNavigate(string route)
		{
			await Shell.Current.GoToAsync(route);
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
			await Navigation.PushAsync(new AddAccountPage(_dbContext, CurrentUser));
		}

		private async void OnAddTransactionClicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new AddTransactionPage(_dbContext));
		}
	}
}
