using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtbavialFinance.Models
{
	public class Account
	{
		public long Id { get; set; } // Изменено на long
		public string Name { get; set; } // Название счета (например, "Наличные", "Карта", "Вклад")
		public decimal Balance { get; set; } // Баланс счета
		public long CurrencyId { get; set; } // Изменено на long
		public Currency Currency { get; set; } // Валюта счета
		public string AccountType { get; set; } // Тип счета (например, "Наличные", "Банковская карта", "Вклад")
	}
}
