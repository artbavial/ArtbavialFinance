using System;
using System.Collections.Generic;
using System.Linq;
using ArtbavialFinance.Models;
using ArtBavialMyFinance.Models;

namespace ArtBavialMyFinance.Services
{
	public class FinanceManager
	{
		private List<User> Users { get; set; }
		private List<Currency> Currencies { get; set; }
		private List<Account> Accounts { get; set; }
		private List<Transaction> Transactions { get; set; }

		public FinanceManager()
		{
			Users = new List<User>();
			Currencies = new List<Currency>();
			Accounts = new List<Account>();
			Transactions = new List<Transaction>();
		}

		public void AddUser(User user)
		{
			user.Id = Users.Count > 0 ? Users.Max(u => u.Id) + 1 : 1;
			Users.Add(user);
		}

		public void AddAccountToUser(long userId, Account account)
		{
			var user = Users.FirstOrDefault(u => u.Id == userId);
			if (user != null)
			{
				account.Id = user.Accounts.Count > 0 ? user.Accounts.Max(a => a.Id) + 1 : 1;
				account.UserId = userId;
				user.Accounts.Add(account);
			}
			else
			{
				throw new Exception("User not found.");
			}
		}

		public void AddTransactionToUser(long userId, Transaction transaction)
		{
			var user = Users.FirstOrDefault(u => u.Id == userId);
			if (user != null)
			{
				transaction.Id = user.Transactions.Count > 0 ? user.Transactions.Max(t => t.Id) + 1 : 1;
				transaction.UserId = userId;
				user.Transactions.Add(transaction);
			}
			else
			{
				throw new Exception("User not found.");
			}
		}

		public void AddCurrencyToUser(long userId, Currency currency)
		{
			var user = Users.FirstOrDefault(u => u.Id == userId);
			if (user != null)
			{
				currency.Id = user.Accounts.Count > 0 ? user.Accounts.Max(c => c.Id) + 1 : 1;
				currency.UserId = userId;
				Currencies.Add(currency);
			}
			else
			{
				throw new Exception("User not found.");
			}
		}

		public List<Currency> GetUserCurrencies(long userId)
		{
			return Currencies.Where(c => c.UserId == userId).ToList();
		}
	}
}
