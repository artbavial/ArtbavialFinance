using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtbavialMyFinance.Models;

namespace ArtbavialFinance.Services
{
		public class FinanceManager
		{
			private List<Account> Accounts { get; set; }
			private List<Transaction> Transactions { get; set; }

			public FinanceManager()
			{
				Accounts = new List<Account>();
				Transactions = new List<Transaction>();
			}

			// Метод для перевода средств между счетами
			public void TransferFunds(long fromAccountId, long toAccountId, decimal amount, decimal? exchangeRate = null)
			{
				var fromAccount = Accounts.FirstOrDefault(a => a.Id == fromAccountId);
				var toAccount = Accounts.FirstOrDefault(a => a.Id == toAccountId);

				if (fromAccount == null || toAccount == null)
				{
					throw new Exception("Один из счетов не найден.");
				}

				if (fromAccount.CurrencyId == toAccount.CurrencyId)
				{
					// Обычный перевод
					if (fromAccount.Balance >= amount)
					{
						fromAccount.Balance -= amount;
						toAccount.Balance += amount;
						LogTransaction(fromAccountId, amount, "Перевод", "Перевод на счет " + toAccount.Name);
					}
					else
					{
						throw new Exception("Недостаточно средств на счете.");
					}
				}
				else
				{
					// Перевод между счетами с разными валютами
					if (exchangeRate.HasValue)
					{
						decimal convertedAmount = ConvertCurrency(amount, exchangeRate.Value);
						if (fromAccount.Balance >= amount)
						{
							fromAccount.Balance -= amount;
							toAccount.Balance += convertedAmount;
							LogTransaction(fromAccountId, amount, "Перевод", "Перевод на счет " + toAccount.Name);
						}
						else
						{
							throw new Exception("Недостаточно средств на счете.");
						}
					}
					else
					{
						throw new Exception("Необходимо указать курс обмена для перевода между разными валютами.");
					}
				}
			}

			// Метод для конвертации валюты
			private decimal ConvertCurrency(decimal amount, decimal exchangeRate)
			{
				return amount * exchangeRate; // Конвертация суммы по заданному курсу
			}

			// Метод для регистрации транзакции
			private void LogTransaction(long accountId, decimal amount, string type, string description)
			{
				Transactions.Add(new Transaction
				{
					Id = Transactions.Count + 1, // Можно изменить на более надежный способ генерации Id
					AccountId = accountId,
					Amount = amount,
					Date = DateTime.Now,
					Type = type,
					Description = description
				});
			}

			// Метод для добавления нового счета
			public void AddAccount(Account account)
			{
				account.Id = Accounts.Count > 0 ? Accounts.Max(a => a.Id) + 1 : 1; // Генерация Id
				Accounts.Add(account);
			}

			// Метод для получения всех счетов
			public List<Account> GetAccounts()
			{
				return Accounts;
			}

			// Метод для получения всех транзакций
			public List<Transaction> GetTransactions()
			{
				return Transactions;
			}
		}

}
