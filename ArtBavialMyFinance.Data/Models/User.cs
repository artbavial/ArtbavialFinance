using ArtBavialMyFinance.Data.Models;
using ArtBavialMyFinance.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtbavialFinance.Models
{
	public class User
	{
		[Key]
		public long Id { get; set; }
		public string Username { get; set; }
		public string Email { get; set; }
		public string PasswordHash { get; set; } // Пример, для реального приложения используйте хешированные пароли
		public List<Account> Accounts { get; set; }
		public List<Transaction> Transactions { get; set; }
		public List<Currency> Currencies { get; set; }
		public List<OperationCategory> OperationCategories { get; set; }
		public List<Counterparty> Counterparties { get; set; }

		public User()
		{
			Accounts = new List<Account>();
			Transactions = new List<Transaction>();
			Currencies = new List<Currency>();
			OperationCategories = new List<OperationCategory>();
			Counterparties = new List<Counterparty>();
		}
	}
}

