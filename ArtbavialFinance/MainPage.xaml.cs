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
using System.Collections.ObjectModel;
using ArtBavialMyFinance.Models;
using ArtBavialMyFinance.Services;

namespace ArtBavialFinance
{
	public partial class MainPage : ContentPage
	{
		private readonly AppDbContext _dbContext;
		public User _currentUser { get; private set; }
		public ICommand NavigateCommand { get; private set; }
		public ObservableCollection<Transaction> Transactions { get; set; }
		private FinanceManager _financeManager = new FinanceManager();

		public MainPage(AppDbContext dbContext, User user)
		{
			InitializeComponent();
			NavigateCommand = new Command<string>(OnNavigate);
			BindingContext = this;
			_dbContext = dbContext;
			_currentUser = user;
			Transactions = new ObservableCollection<Transaction>();
			TransactionsListView.ItemsSource = Transactions;
		}

		protected override async void OnAppearing()
		{
			base.OnAppearing();
			await LoadDashboardData();
		}

		private async void OnNavigate(string route)
		{
			await Shell.Current.GoToAsync(route);
		}

		private async Task LoadDashboardData()
		{
			await LoadTotalBalance();
			await LoadRecentTransactions();
		}

		private async Task LoadTotalBalance()
		{
			var primaryAccount = await _dbContext.Accounts
												 .Where(a => a.UserId == _currentUser.Id && a.IsPrimaryAccount)
												 .Include(c => c.User)
												 .Include(c => c.Currency)
												 .FirstOrDefaultAsync();
			if (primaryAccount != null)
			{
				var totalBalance = primaryAccount.Balance;
				TotalBalanceLabel.Text = $"{totalBalance:F2} {primaryAccount.Currency?.Symbol}";
			}
			else
			{
				TotalBalanceLabel.Text = "Добавьте основной счет для отображения данных";
			}
		}

		private async Task LoadRecentTransactions()
		{
			using (var context = new AppDbContext())
			{
				Transactions.Clear();
				var recentTransactions = await context.Transactions
														  .Where(t => t.UserId == _currentUser.Id)
														  .OrderByDescending(t => t.Date)
														  .Take(10)
														  .ToListAsync();
				foreach (var transaction in recentTransactions)
				{
					Transactions.Add(transaction);
				}

			}
		}

		private async void OnAddAccountClicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new AddAccountPage(_dbContext, _currentUser));
		}

		private async void OnAddTransactionClicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new AddTransactionPage(_dbContext, _currentUser));
		}

		private async void OnShowCurrenciesClicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new ManageCurrenciesPage(_financeManager, _currentUser));
		}
	}
}
